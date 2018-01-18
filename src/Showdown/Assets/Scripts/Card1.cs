using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card1 : MonoBehaviour {

	// Responsible for telling the game when none of the cards should be flipped
	public static bool DO_NOT = false;

	[SerializeField]
	// If state equals 0 card's back is visible
	// If state equals 1 card's face is visible
	private int _state;
	[SerializeField]
	private int _cardValue;
	[SerializeField]
	private bool _initialized = false;

	private Sprite _cardBack;
	private Sprite _cardFace;

	private GameObject _manager;

	void Start() {
		_state = 1;
		_manager = GameObject.FindGameObjectWithTag ("Manager");
	}

	// Initializing all the values
	public void setupGraphics() {
		_cardBack = _manager.GetComponent<GameManager1> ().getCardBack ();
		_cardFace = _manager.GetComponent<GameManager1> ().getCardFace (_cardValue);

		flipCard ();
	}

	// Cards being able to flip
	public void flipCard() {

		if (_state == 0)
			_state = 1;
		else if (_state == 1)
			_state = 0;

		if (_state == 0 && !DO_NOT)
			GetComponent<Image> ().sprite = _cardBack;
		else if (_state == 1 && !DO_NOT)
			GetComponent<Image> ().sprite = _cardFace;
	}
		
	public int cardValue {
		get { return _cardValue; }
		set { _cardValue = value; }
	}
		
	public int state {
		get { return _state; }
		set { _state = value; }
	}

	public bool initialized {
		get { return _initialized; }
		set { _initialized = value; }
	}

	//It gives a little buffer for when cards are flipped over and you check to see if they are a match
	public void falseCheck() {
		StartCoroutine (pause ());
	}

	// How long you should wait before you flip them back over
	IEnumerator pause() {
		yield return new WaitForSeconds (1);
		if (_state == 0)
			GetComponent<Image> ().sprite = _cardBack;
		else if (_state == 1)
			GetComponent<Image> ().sprite = _cardFace;
		DO_NOT = false;
	}
}
