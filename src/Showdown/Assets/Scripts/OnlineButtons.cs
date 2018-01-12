using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OnlineButtons : MonoBehaviour {

	public void triggerMenuBehavior(int i) {
		switch (i) {
		default:
		case(0):
			SceneManager.LoadScene ("Showdown2Players");	
			break;
		}

	}
}
