using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Text gameContent;
	private bool tooMuchWii;

	// Use this for initialization
	void Start () {
		gameContent.text = "WIIIIII ANDA!!!";
		tooMuchWii = false;
	}

	public void Wii () {
		gameContent.text += "\n Wii!";
	}

	public void Quit () {
		// TODO : DO IT RIGHT!!!
		Application.Quit();
	}
}
