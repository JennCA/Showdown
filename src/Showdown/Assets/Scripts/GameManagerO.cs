using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerO : MonoBehaviour {

	public static GameManagerO Instance { get; set; }
	public Text p1;
	public Text p2;
	public Sprite[] cardFace;
	public Sprite cardBack;
	public GameObject[] cards;
	public Text matchText;
	public string playername1;
	public string playername2;

	private Client client;

	private bool _init = false;
	private int _matches = 13;
	private int score1 = 0;
	private int score2 = 0;
	private bool myTurn = true;
	private Vector2 mouseClick;
	
	// Update is called once per frame
	void Update () {
		if (!_init) {
			
			initializeCards ();

		}

		if (Input.GetMouseButtonUp (0)) {
			checkCards ();
		}

	}
	public void initDefinedCards(string cardsPosition) {
		if (client == null && client.isHost) {
			return;
		}

		string[] cardValues = cardsPosition.Split (',');

		for (int i = 0; i < cardValues.Length; i++) {
			cards [i].GetComponent<CardO> ().cardValue = int.Parse(cardValues[i]);
			cards [i].GetComponent<CardO> ().initialized = true;
		}
		int cardIndex = 0;
		foreach (GameObject c in cards) {
			c.GetComponent<CardO> ().cardIndex = cardIndex++;
			c.GetComponent<CardO> ().setupGraphics ();
			c.GetComponent<CardO> ().setupGameManager (this);
		}
		if (!_init)
			_init = true;

		checkCards ();
	}
	public void tryFlip(int cardIndex) {
		
		cards [cardIndex].GetComponent<CardO> ().forceFlipCard ();
		checkCards ();
	}	
	public bool canFlip(int cardIndex) {
		Debug.Log ("CanFlip index: " + cardIndex);
		if (!myTurn) {
			return false;
		}
		string msg = "CFLIP|";
		msg +=cardIndex+ "|";
		client.Send (msg);

		return true;   
	}

	void initializeCards() {
		for (int id = 0; id < 2; id++) {
			for (int i = 1; i < 14; i++) {
			
				bool test = false;
				int choice = 0;
				while (!test) {
					choice = Random.Range (0, cards.Length);
					test = !(cards [choice].GetComponent<CardO> ().initialized);
				}
				cards [choice].GetComponent<CardO> ().cardValue = i;
				cards [choice].GetComponent<CardO> ().initialized = true;
			}
		}


		int cardIndex = 0;
		foreach (GameObject c in cards) {
			c.GetComponent<CardO> ().cardIndex = cardIndex++;
			c.GetComponent<CardO> ().setupGraphics ();
			c.GetComponent<CardO> ().setupGameManager (this);
		}

		string cardIds = "";
		for (int i = 0; i < cards.Length; i++) {
			if (cardIds.Length != 0) {
				cardIds += ",";
			}
			cardIds += cards [i].GetComponent<CardO> ().cardValue;
		}
		client.Send ("CARDS|" + cardIds);

		Debug.Log ("Cards : " + cardIds);
		if (!_init)
			_init = true;
		
	}

	public Sprite getCardBack() {
		return cardBack;
	}

	public Sprite getCardFace(int i) {
		return cardFace [i - 1];
	}

	void checkCards() {

		List<int> c = new List<int> ();

		for (int i = 0; i < cards.Length; i++) {
			if (cards [i].GetComponent<CardO> ().state == 1)
				c.Add (i);
		}

		if (c.Count == 2)
			cardComparison (c);
	}

	void cardComparison(List<int> c) {
		
		int x = 0;

		if (cards [c [0]].GetComponent<CardO> ().cardValue == cards [c [1]].GetComponent<CardO> ().cardValue) {
			x = 2;
			_matches--;
			matchText.text = "Number of Matches: " + _matches;
			if (client.isHost) {
				if (myTurn) {
					score1++;
				} else {
					score2++;
				}

			} 
			else {
				if (!myTurn) {
					score1++;
				}
				else {
					score2++;
				}

			}
				
			p1.text = playername1 + ": " + score1;
			p2.text = playername2 + ": " + score2;
			if (_matches == 0)
				SceneManager.LoadScene ("Menu");
		}
		else {
			myTurn = !myTurn;
		}

		HighlightPlayer ();


		for (int i = 0; i < c.Count; i++) {
			cards [c [i]].GetComponent<CardO> ().state = x;
			cards [c [i]].GetComponent<CardO> ().falseCheck ();
		}
	}

	private void Start()
	{
		Instance = this;
		client = FindObjectOfType<Client> ();
		myTurn = client.isHost;
		SetPlayerNames(client.players [0].name, client.players [1].name);

		HighlightPlayer ();
			
	}

	public void SetPlayerNames(string name1, string name2) {
		playername1 = name1;
		playername2 = name2;

		p1.text = playername1 + ": " + score1;
		p2.text = playername2 + ": " + score2;
	}

	private void HighlightPlayer(){
		if (client.isHost) {
			if (myTurn) {
				p1.color = Color.magenta;
				p2.color = Color.black;
			}
			else {
				p1.color = Color.black;
				p2.color = Color.magenta;
			}
		} 
		else {
			if (!myTurn) {
				p1.color = Color.magenta;
				p2.color = Color.black;
			}
			else {
				p1.color = Color.black;
				p2.color = Color.magenta;
			}
		}

	}
}
