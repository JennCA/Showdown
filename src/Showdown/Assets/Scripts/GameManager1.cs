using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour {

	public Text p1;
	public Text p2;
	public Sprite[] cardFace;
	public Sprite cardBack;
	public GameObject[] cards;
	public Text matchText;
	public Text winnerText;

	private bool _init = false;
	private int _matches = 13;
	private int score1 = 0;
	private int score2 = 0;
	private bool myTurn = true;
	private GameObject button;

	void Start (){
		winnerText.enabled = false;
		button = GameObject.Find ("Exit Button");
		button.SetActive (false);
	}

	// Update is called once per frame
	void Update () 
	{
		if (!_init)
			initializeCards ();

		if (Input.GetMouseButtonUp (0))
			checkCards ();
	}

	void initializeCards() 
	{
		for (int id = 0; id < 2; id++) {
			for (int i = 1; i < 14; i++) {

				bool test = false;
				int choice = 0;
				while (!test) {
					choice = Random.Range (0, cards.Length);
					test = !(cards [choice].GetComponent<Card1> ().initialized);
				}
				cards [choice].GetComponent<Card1> ().cardValue = i;
				cards [choice].GetComponent<Card1> ().initialized = true;
			}		
		}

		foreach (GameObject c in cards)
			c.GetComponent<Card1> ().setupGraphics ();

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
			if (cards [i].GetComponent<Card1> ().state == 1)
				c.Add (i);
		}

		if (c.Count == 2)
			cardComparison (c);
	}

	void cardComparison(List<int> c){
		Card1.DO_NOT = true;
		Debug.Log ("cardComparison");
		int x = 0;

		if (cards [c [0]].GetComponent<Card1> ().cardValue == cards [c [1]].GetComponent<Card1> ().cardValue) {
			x = 2;
			Debug.Log ("c1:" + cards [c [0]].GetComponent<Card1> ().cardValue );
			Debug.Log ("c2:" + cards [c [1]].GetComponent<Card1> ().cardValue );
			_matches--;
			matchText.text = "Number of Matches: " + _matches;
			if (myTurn) {
				score1++;
			}
			else {
				score2++;
			}
			p1.text = "player1: " + score1;
			p2.text = "player2: " + score2;
			if (_matches == 0) {
				if (score1 > score2) {
					winnerText.text = "The Winner is player1";  
				} 
				else {
					winnerText.text = "The Winner is player2";  
				}
				//show winner
				winnerText.enabled = true;
			
				button.SetActive (true);
				//exit button
			}
		

			
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
			cards [c [i]].GetComponent<Card1> ().state = x;
			cards [c [i]].GetComponent<Card1> ().falseCheck ();
		}
	}
	public void ExitGame(){
		Application.Quit ();
	}
}
	