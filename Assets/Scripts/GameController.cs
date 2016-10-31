using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public void NewGame(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void Quit () {
		Application.Quit();
	}
}
