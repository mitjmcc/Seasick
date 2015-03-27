using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Button play, settings, credits, exit;
	public LoadingScreen load;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartGame() {
		StartCoroutine (load.GetComponent<LoadingScreen> ().DisplayLoadingScreen ("Main"));
	}

	public static void Options() {

	}

	public static void Credits() {

	}

	public static void Exit() {
		Application.Quit ();
	}
}
