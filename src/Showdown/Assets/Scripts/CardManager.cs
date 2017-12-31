using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour {

	public int numberOfCards = 80;
	public string[] cards;


	// Use this for initialization
	void Start () {

		#region Karten einlesen
		cards = new string[numberOfCards];
		int cardNumber =  1;

		for (int i = 0; i < numberOfCards; i++) 
		{
			//Takto to vyzera pri zolikovych kartach (srdcovy)
			if (i <= 20) {
				if (cardNumber == 1) {cards [i] = "HerzAss";}
				else if (cardNumber > 1 && cardNumber <= 10) {cards [i] = "Herz" + cardNumber;}
				if (cardNumber == 11) {cards [i] = "HerzBube";}
				if (cardNumber == 12) {cards [i] = "HerzDame";}
				if (cardNumber == 13) {cards [i] = "HerzKönig";	cardNumber = 0;}
			}
				//karovy
			if (i > 20 && i<= 40) {
				if (cardNumber == 1) {cards [i] = "HerzAss";}
				else if (cardNumber > 1 && cardNumber <= 10) {cards [i] = "Herz" + cardNumber;}
				if (cardNumber == 11) {cards [i] = "HerzBube";}
				if (cardNumber == 12) {cards [i] = "HerzDame";}
				if (cardNumber == 13) {cards [i] = "HerzKönig";	cardNumber = 0;}
			}
				//listovy
			if (i>40 && i<=60) {
				if (cardNumber == 1) {cards [i] = "HerzAss";}
				else if (cardNumber > 1 && cardNumber <= 10) {cards [i] = "Herz" + cardNumber;}
				if (cardNumber == 11) {cards [i] = "HerzBube";}
				if (cardNumber == 12) {cards [i] = "HerzDame";}
				if (cardNumber == 13) {cards [i] = "HerzKönig";	cardNumber = 0;}
			}
				//zaludovy (gulovy)
			if (i>40 && i<=80) {
				if (cardNumber == 1) {cards [i] = "HerzAss";}
				else if (cardNumber > 1 && cardNumber <= 10) {cards [i] = "Herz" + cardNumber;}
				if (cardNumber == 11) {cards [i] = "HerzBube";}
				if (cardNumber == 12) {cards [i] = "HerzDame";}
				if (cardNumber == 13) {cards [i] = "HerzKönig";	cardNumber = 0;}
			}

			cardNumber++;
		}
		#endregion
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.M)) 
		{
			ShuffleCards (cards);
		}
	}

	void ShuffleCards (string[] cards)
	{
		for (int i = 0; i < cards.Length; i++) 
		{
			int r = Random.Range(0, cards.Length-1);
			string tmp = cards[i];
			cards[i] = cards[r];
			cards[r] = tmp;
		}
	}
	public void ButtonInteract(){
		Debug.Log ("Our button was clicked");

	}
}
