using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuBehavior : MonoBehaviour {

	public void triggerMenuBehavior(int i) {
		switch (i) {
		default:
		case(0):
			SceneManager.LoadScene ("G_Modes");	
			break;
		case(1):
			Application.Quit ();
			break;
		}

	}
}
