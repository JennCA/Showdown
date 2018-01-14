using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; set; }
	public Text p1;
	public Text p2;
	public Sprite[] cardFace;
	public Sprite cardBack;
	public GameObject[] cards;
	public Text matchText;

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
			if (client != null && client.isHost) {
				initializeCards ();
			} else {
				initializeCards ();
			}
		}
	/*
		if (Input.GetMouseButtonUp (0)) {
			checkCards ();
		}
		*/
	}
	public void initDefinedCards(string cardsPosition) {
		if (client == null && client.isHost) {
			// ignore
			return;
		}

		string[] cardValues = cardsPosition.Split (',');

		for (int i = 0; i < cardValues.Length; i++) {
			cards [i].GetComponent<Card> ().cardValue = int.Parse(cardValues[i]);
			cards [i].GetComponent<Card> ().initialized = true;
		}
		int cardIndex = 0;
		foreach (GameObject c in cards) {
			c.GetComponent<Card> ().cardIndex = cardIndex++;
			c.GetComponent<Card> ().setupGraphics ();
			c.GetComponent<Card> ().setupGameManager (this);
		}
		if (!_init)
			_init = true;

		checkCards ();
	}
	public void tryFlip(int cardIndex) {
		/*
		if (myTurn)
			//ignore
			return;
			*/
		cards [cardIndex].GetComponent<Card> ().forceFlipCard ();
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
					test = !(cards [choice].GetComponent<Card> ().initialized);
				}
				cards [choice].GetComponent<Card> ().cardValue = i;
				cards [choice].GetComponent<Card> ().initialized = true;
			}
		}


		int cardIndex = 0;
		foreach (GameObject c in cards) {
			c.GetComponent<Card> ().cardIndex = cardIndex++;
			c.GetComponent<Card> ().setupGraphics ();
			c.GetComponent<Card> ().setupGameManager (this);
		}

		string cardIds = "";
		for (int i = 0; i < cards.Length; i++) {
			if (cardIds.Length != 0) {
				cardIds += ",";
			}
			cardIds += cards [i].GetComponent<Card> ().cardValue;
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
			if (cards [i].GetComponent<Card> ().state == 1)
				c.Add (i);
		}

		if (c.Count == 2)
			cardComparison (c);
	}

	void cardComparison(List<int> c) {
		//Card.DO_NOT = true;

		int x = 0;

		if (cards [c [0]].GetComponent<Card> ().cardValue == cards [c [1]].GetComponent<Card> ().cardValue) {
			x = 2;
			_matches--;
			matchText.text = "Number of Matches: " + _matches;
			//p1.text = "PLAYER 1: " + _matches;
			if (myTurn) {
				score1++;
			}
			else {
				score2++;
			}
			p1.text = "player1: " + score1;
			p2.text = "player2: " + score2;
			if (_matches == 0)
				SceneManager.LoadScene ("Menu");
		}
		else {
			myTurn = !myTurn;
		}

		if (myTurn) {
			p1.color = Color.magenta;
			p2.color = Color.black;
		}
		else {
			p1.color = Color.black;
			p2.color = Color.magenta;
		}

		for (int i = 0; i < c.Count; i++) {
			cards [c [i]].GetComponent<Card> ().state = x;
			cards [c [i]].GetComponent<Card> ().falseCheck ();
		}
	}

	private void Start()
	{
		Instance = this;
		client = FindObjectOfType<Client> ();
		myTurn = client.isHost;
		if (myTurn) 
		{
			
		}
			
	}
}
