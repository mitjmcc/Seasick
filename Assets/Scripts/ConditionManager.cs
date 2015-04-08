using UnityEngine;
using System.Collections;

public class ConditionManager : MonoBehaviour {
	
	public GameObject ship;
	public GameObject[] enemyShips;

	public bool gameover;
	public bool won;
	public bool lost;

	public int day;
	public int oldDay;
	public int dayOfReckoning = 7;

	public string GOMessage1 = "The Royal Navy has arrived and you did not rebuild the ship!";
	public string GOMessage2 = "Your crew has decided to mutiny!";

	public Material mast1, mast2;
	public Color color1, color2;
	public GameObject GameOverScreen;
	public GameObject WinScreen;

	public static ConditionManager instance { get; private set; }

	void Awake ()
	{
		GameOverScreen.SetActive (false);
		GameOverScreen.transform.GetChild(1).gameObject.SetActive(false);
		GameOverScreen.transform.GetChild(2).gameObject.SetActive(false);
		instance = this;
		color1 = mast1.color;
		color2 = mast2.color;
	}
	
	void Update () {
		if (DayNightController.daysPast > dayOfReckoning) {
			shipsAppear ();
			day = DayNightController.daysPast;
		}



		checkGameOver ();
		updateMasts ();
	}

	public bool checkMorale() {
		if (DataValues.instance.totalMorale <= 0) {
			lost = true;
			return true;
		}
		return false;
	}

	public bool anyDaysLeft() {
		bool e = DayNightController.daysPast >= DataValues.instance.defaultDays;
		Debug.Log ("Checking Days: " + e + " " + DayNightController.daysPast + " / " + DataValues.instance.defaultDays);
		if (DayNightController.daysPast >= DataValues.instance.defaultDays) {
			if (DataValues.instance.repair >= DataValues.instance.maxRepair - 5) {
				won = true;
				Debug.Log ("You won!");
			} else {
				lost = true;
				Debug.Log ("You lost!");
			}
			return true;
		}
		return false;
	}

	public void checkGameOver() {
		bool day = anyDaysLeft ();
		bool mutiny = checkMorale ();
		if (day || mutiny) {
			GameOverScreen.SetActive(true);
			if (won)
				WinScreen.SetActive(true);
			gameover = true;
		}
		if (day) {
			if (lost)
				GameOverScreen.transform.GetChild(2).gameObject.SetActive(true);
			Debug.Log (GOMessage1);
			
		} else if (mutiny) {
			GameOverScreen.transform.GetChild(1).gameObject.SetActive(true);
			Debug.Log (GOMessage2);
		}
	}

	public void shipsAppear() {
		foreach (GameObject g in enemyShips)
			g.SetActive (true);
	}

	public void updateMasts() {
		Debug.Log (DataValues.instance.repair / DataValues.instance.maxRepair);
		if (DataValues.instance.repair / DataValues.instance.maxRepair > .15) {
			Color temp = color1;
			temp.a = 1f;
			mast1.color = temp;
			Debug.Log ("Yelp");
		}
		if (DataValues.instance.repair / DataValues.instance.maxRepair > .75) {
			Color temp2 = color2;
			temp2.a = 1f;
			mast2.color = temp2;
		}
	}

	void OnDestroy() {
		Color temp = color1;
		temp.a = 0f;
		mast1.color = temp;

		Color temp2 = color2;
		temp2.a = 0f;
		mast2.color = temp2;

		GameOverScreen.SetActive(false);
		WinScreen.SetActive(false);
		
	}
}
