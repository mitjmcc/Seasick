using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pirate : MonoBehaviour
{
	public string name;
	
	public Animator anim;
	public NavMeshAgent agent;

	public float hunger;
	public float thirst;
	public float morale;
	public double jobStartTime;

	private float maxHunger;
	private float maxThirst;
	private float maxMorale;

	public Vector3 origLocation;
	public Vector3 curLocation;
	public Vector3 lastLocation;

	public bool selected;
	public bool doneJob = false;
	public bool hungry;
	public bool thirsty;
	public bool lowMor;

	public Job lastJob;
	private GameObject statAnimator;
	public GameObject[] icons;
	public AudioClip[] pirateSpeechClips;

	//Initialization
	void Start ()
	{
		maxHunger = DataValues.instance.getMaxHunger ();
		maxThirst = DataValues.instance.getMaxThirst ();
		maxMorale = DataValues.instance.getMaxMorale ();

		hunger = maxHunger;
		thirst = maxThirst;
		morale = maxMorale;

		agent = gameObject.GetComponent<NavMeshAgent> ();
		origLocation = gameObject.transform.position;
		curLocation = gameObject.transform.position;
		lastLocation = gameObject.transform.position;

		anim = gameObject.GetComponent<Animator> ();
		statAnimator = GameObject.Find ("StatBars");

		foreach (GameObject g in icons) {
			g.SetActive (false);
			g.GetComponent<LockRotation>().SetParentTransform(transform);
		}
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
		UpdateIcons ();

		lastLocation = curLocation;
	}

	private void HungerAndThirst() {
		if (DayNightController.minutes == 59 && DayNightController.worldTimeHour % 1 == 0) {
			updateValues (true, true, false, false, -.1f);
			if (hunger <= 0 || thirst <= 0)
				updateValues(false, false, true, false, -.01f);
		}

		if (hunger <= maxHunger / 2)
			hungry = true;
		else
			hungry = false;
		if (thirst <= maxThirst / 2)
			thirsty = true;
		else
			thirsty = false;
		if (morale <= maxMorale / 2)
			lowMor = true;
		else
			lowMor = false;
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

	public void UpdateIcons() {
		icons [0].SetActive (hungry ? true : false);
		icons [0].GetComponent<LockRotation> ().enabled = hungry ? true : false;
		icons [1].SetActive (thirsty ? true : false);
		icons [1].GetComponent<LockRotation> ().enabled = thirsty ? true : false;
		icons [2].SetActive (lowMor ? true : false);
		icons [2].GetComponent<LockRotation> ().enabled = lowMor ? true : false;

		if (selected) {
			if (hungry) {
				icons[0].transform.SetParent(GameObject.Find ("Eating").transform);
				icons[0].GetComponent<LockRotation>().SetParentTransform(GameObject.Find ("Eating").transform);
			}

			if (thirsty) {
				icons[1].transform.SetParent(GameObject.Find ("Drinking").transform);
				icons[1].GetComponent<LockRotation>().SetParentTransform(GameObject.Find ("Drinking").transform);
			}
		}
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