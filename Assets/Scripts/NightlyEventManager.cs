using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NightlyEventManager : MonoBehaviour {

	public NightlyEvent[] events;
	
	public Text currentDay;
	public NightlyEvent lastEvent;
	public GameObject book;

	//We make a static variable to our MusicManager instance
	public static NightlyEventManager instance { get; private set; }
	
	//When the object awakens, we assign the static variable
	void Awake() 
	{
		instance = this;
	}

	void Update () {
		if (Input.GetKeyDown ("return")) {
			deactivateEvent ();
		}
	}

	public NightlyEvent pickEvent() {
		return events [Random.Range (0, events.Length)];
	}

	public void activateEvent() {
		Time.timeScale = 0;
		lastEvent = pickEvent ();
		lastEvent.GetComponent<Text> ().enabled = true;
		currentDay.text = "Day: " + (DayNightController.daysPast + 1);
		currentDay.enabled = true;
		//book.enabled = true;
		book.SetActive(true);
		
		lastEvent.doAffect ();
	}

	public void deactivateEvent() {
		Time.timeScale = 1;
		lastEvent.GetComponent<Text> ().enabled = false;
		book.SetActive(false);
		currentDay.enabled = false;
	}
}
