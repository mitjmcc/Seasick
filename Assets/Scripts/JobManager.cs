using UnityEngine;
using System.Collections;

public class JobManager : MonoBehaviour {

	private static GameObject[] jobObjects;

	public static int totalFood = 200;
	public static int totalWater = 200;
	public static int totalWood = 0;
	public static int defaultDays = 30;
	public static ArrayList jobs;

	void Awake () {
		jobs = new ArrayList ();
		jobObjects = GameObject.FindGameObjectsWithTag ("Jobs");
		for (int i = 0; i < jobObjects.Length; i++)
			jobs.Add(jobObjects[i].gameObject.GetComponent("Job"));
	}

	void Update () {
		checkForJob ();
	}

	public static void calculateMoralEffects() {
		//TODO: Call this function at the beginning of each day

	}

	public static void setFood(int effect) {
		totalFood += effect;
		Debug.Log (totalFood);
	}
	
	public static void setWater(int effect) {
		totalWater += effect;
		Debug.Log (totalWater);
	}
	
	public static void setWood(int effect) {
		totalWood += effect;
		Debug.Log (totalWood);
	}

	///Go through all Jobs and all pirates and checks for an intersection
	///If there is an intersection it will call doAffect which increases/decreases the appopriate stat
	///I think it calls do affect multiple times, so that needs to be sorted out.
	public static void checkForJob() {
		foreach (GameObject j in jobObjects) {
			foreach(GameObject p in PirateManager.getPirateObjects()) {
				if (j.GetComponent<BoxCollider>().bounds.Intersects(p.GetComponent<BoxCollider>().bounds)
				    && !p.GetComponent<Pirate>().doneJob) {
					j.GetComponent<Job>().doAffect();
					p.GetComponent<Pirate>().doneJob = true;
					if (j.GetComponent<Job>().effect < 0)
						p.GetComponent<Pirate>().updateValues(j.GetComponent<Job>().affectsFood,
						                                      j.GetComponent<Job>().affectsWater,
						                                      j.GetComponent<Job>().affectsMorale,
						                                      j.GetComponent<Job>().effect / 5);
				}
			}
		}
	}
}
