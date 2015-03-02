using UnityEngine;
using System.Collections;

public class Job : MonoBehaviour
{
	public int effect;

	public bool affectsFood = false;
	public bool affectsWater = false;
	public bool affectsMorale = false;
	public bool affectsWood = false;
	public bool boatJob = false;

	public Vector3 origLocation;

	public double jobLength;

	public Pirate lastPirate;

	void Start ()
	{
		origLocation = gameObject.transform.position;
	}

	void Update ()
	{
		if (isJobDone ())
			PirateManager.instance.pirateJobReset(lastPirate);
	}

	public void doAffect ()
	{
		if (affectsFood) {
			JobManager.setFood (effect);
			DataValues.instance.setTotalHunger (effect);
		} else if (affectsWater) {
			JobManager.setWater (effect);
			DataValues.instance.setTotalThirst (effect);
		} else if (affectsMorale) {

		} else if (affectsWood) 
			JobManager.setWood (effect);
		DataValues.instance.calculateTotals ();
	}

	public bool isJobDone() {
		if (lastPirate != null) {
			if (DayNightController.GetCurrentTime () > lastPirate.jobStartTime + jobLength)
				return true;
		}
		return false;
	}
}
