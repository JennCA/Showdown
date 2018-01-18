using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardO : MonoBehaviour {

	[SerializeField]
	private int _state;
	[SerializeField]
	private int _cardValue;
	[SerializeField]
	private bool _initialized = false;

	private Sprite  _cardBack;
	private Sprite _cardFace;

	private GameObject _manager;
	private GameManagerO _gm;

	private int _cardIndex;

	void Start() {
		_state = 0;
		_manager = GameObject.FindGameObjectWithTag ("Manager");
	}

	// Initializing all the values
	public void setupGraphics() {
		_cardBack = _manager.GetComponent<GameManagerO> ().getCardBack ();
		_cardFace = _manager.GetComponent<GameManagerO> ().getCardFace (_cardValue);


	}
	public void setupGameManager(GameManagerO gm) {
		_gm = gm;
	}

	// Cards being able to flip
	public void flipCard ()
	{
		if (!_gm.canFlip (_cardIndex)) { 
			return;
		}
	}

	public void forceFlipCard ()
	{
	if (_state == 0)
		_state = 1;
		else if (_state == 1)
			_state = 0;
		
		if (_state == 0)
			GetComponent<Image> ().sprite = _cardBack;
		else if (_state == 1)
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
	public int cardIndex {
		get { return _cardIndex; }
		set { _cardIndex = value; }
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
	}
}
