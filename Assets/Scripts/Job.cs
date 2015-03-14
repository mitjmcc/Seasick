using UnityEngine;
using System.Collections;

public class Job : MonoBehaviour
{
	public int effect;

	public bool affectsFood = false;
	public bool affectsWater = false;
	public bool affectsMorale = false;
	public bool affectsWood = false;
	public bool affectsRepair = false;
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
			DataValues.instance.setFood (effect);
			DataValues.instance.setTotalHunger (effect);
		} else if (affectsWater) {
			DataValues.instance.setWater (effect);
			DataValues.instance.setTotalThirst (effect);
		} else if (affectsMorale) {

		} else if (affectsWood) 
			DataValues.instance.setWood (effect);
		if (affectsRepair) {
			DataValues.instance.setRepair(-effect);
		}
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
