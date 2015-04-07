﻿using UnityEngine;
using System.Collections;

public class JobManager : MonoBehaviour
{

	private static GameObject[] jobObjects;

	public static ArrayList jobs;

	void Awake ()
	{
		jobs = new ArrayList ();
		jobObjects = GameObject.FindGameObjectsWithTag ("Jobs");
		for (int i = 0; i < jobObjects.Length; i++)
			jobs.Add (jobObjects [i].gameObject.GetComponent ("Job"));
	}

	void Update ()
	{
		checkForJob ();
	}

	public static void calculateMoralEffects ()
	{
			//TODO: Call this function at the beginning of each day

	}

	///Go through all Jobs and all pirates and checks for an intersection
	///If there is an intersection it will call doAffect which increases/decreases the appopriate stat
	///I think it calls do affect multiple times, so that needs to be sorted out.
	public static void checkForJob ()
	{
		foreach (GameObject j in jobObjects) {
			foreach (GameObject p in PirateManager.instance.getPirateObjects()) {
				if (j.GetComponent<BoxCollider> ().bounds.Intersects (p.GetComponent<BoxCollider> ().bounds)
					&& !p.GetComponent<Pirate> ().doneJob) {
					j.GetComponent<Job> ().lastPirate = p.GetComponent<Pirate> ();
					j.GetComponent<Job> ().doAffect ();
					p.GetComponent<Pirate> ().doneJob = true;
					p.GetComponent<Pirate> ().jobStartTime = DayNightController.GetCurrentTime();
					updateValues (j.GetComponent<Job> (), p.GetComponent<Pirate> ());
					if (j.GetComponent<Job> ().boatJob)
						boating (j, p);
					//p.GetComponent<Pirate> ().agent.enabled = false;
					p.GetComponent<Pirate> ().lastJob = j.GetComponent<Job> ();
				}
			}
		} 
	}

	public static void updateValues (Job j, Pirate p)
	{
		if (j.effect < 0)
			p.updateValues (j.GetComponent<Job> ().affectsFood,
      			j.GetComponent<Job> ().affectsWater,
  				j.GetComponent<Job> ().affectsMorale,
			    false,
      			j.GetComponent<Job> ().effect / -5);
	}

	public static void boating (GameObject j, GameObject p)
	{
		j.GetComponent<Transform> ().position = new Vector3 (2.36f, -15.4f, 77.1f);
		p.GetComponent<Transform> ().position = new Vector3 (2.36f, -15.4f, 77.1f);
	}
}
