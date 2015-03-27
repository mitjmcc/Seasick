using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pirate : MonoBehaviour
{
	public NavMeshAgent agent;

	public float hunger;
	public float thirst;
	public float morale;

	private float maxHunger;
	private float maxThirst;
	private float maxMorale;

	public Vector3 origLocation;
	public Vector3 curLocation;
	public Vector3 lastLocation;

	public bool selected = false;
	public bool doneJob = false;

	public Job lastJob;
	public double jobStartTime;

	public string name;

	public AudioClip[] pirateSpeechClips;

	public Animator anim;

	private GameObject statAnimator;

	//Initialization
	void Start ()
	{
		maxHunger = DataValues.instance.getMaxHunger ();
		maxThirst = DataValues.instance.getMaxThirst ();
		maxMorale = DataValues.instance.getMaxMorale ();
		hunger = (int) maxHunger;
		thirst = (int) maxThirst;
		morale = (int) maxMorale;
		agent = gameObject.GetComponent<NavMeshAgent> ();
		origLocation = gameObject.transform.position;
		curLocation = gameObject.transform.position;
		lastLocation = gameObject.transform.position;
		anim = gameObject.GetComponent<Animator> ();
		statAnimator = GameObject.Find ("StatBars");
	}

	void Update ()
	{
		curLocation = gameObject.transform.position;

		if (Vector3.Distance (curLocation, agent.destination) <= 1f) {
			agent.updatePosition = false;
			agent.updateRotation = false;
			anim.SetBool ("walk", false);
		} else {
			//agent.enabled = true;
			anim.SetBool ("walk", true);
		} 

		if (selected) {
			updateUI ();
		}	

		HungerAndThirst ();

		lastLocation = curLocation;
	}

	private void HungerAndThirst() {
		if (DayNightController.minutes == 59 && DayNightController.worldTimeHour % 1 == 0) {
			updateValues (true, true, false, false, -1);
			if (hunger <= 0 || thirst <= 0)
				updateValues(false, false, true, false, -1);
		}
	}

	public void say (int audioIndex)
	{
		if (!GetComponent<AudioSource>().isPlaying) {
			GetComponent<AudioSource>().clip = pirateSpeechClips [audioIndex];
			GetComponent<AudioSource>().pitch = Random.Range (1.0F, 1.15F);
			GetComponent<AudioSource>().Play ();
		}
	}

	public void updateUI ()
	{
		statAnimator.GetComponent<StatBarAnimator> ().changeHunger (hunger);
		statAnimator.GetComponent<StatBarAnimator> ().changeThirst (thirst);
		statAnimator.GetComponent<StatBarAnimator> ().changeMorale (morale);
		//Sprite [] pirateFaces = GameObject.Find ("PirateManager").GetComponent<PirateManager> ().pirateFaces;
		//GameObject.Find ("StatBars").GetComponent<StatBarAnimator> ().changeFaces (pirateFaces);
	}

	public void updateValues (bool h, bool t, bool m, bool reset, float delta)
	{
		if (h) {
			if (hunger + delta <= 0)
				hunger = 0;
			else if (hunger + delta >= maxHunger)
				hunger = (int) maxHunger;
			else if (reset)
				hunger = (int) maxHunger;
			else
				hunger = hunger + delta;
		}
		
		if (t) {
			if (thirst + delta <= 0)
				thirst = 0;
			else if (thirst + delta >= maxThirst)
				thirst = maxThirst;
			else if (reset)
				thirst = maxThirst;
			else
				thirst = thirst + delta;
		}
		
		if (m) {
			if (morale + delta <= 0)
				morale = 0;
			else if (morale + delta >= maxMorale)
				morale = maxMorale;
			else if (reset)
				morale = maxMorale;
			else
				morale = morale + delta;
		}
	}
}