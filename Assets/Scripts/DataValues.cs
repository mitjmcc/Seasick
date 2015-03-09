using UnityEngine;
using System.Collections;

public class DataValues : MonoBehaviour
{
	public int totalHunger = 0;
	public int totalThirst = 0;
	public int totalMorale = 0;

	public int totalFood = 200;
	public int totalWater = 200;
	public int totalWood = 0;
	
	public int maxHunger = 20;
	public int maxThirst = 15;
	public int maxMorale = 50;
	public int maxRepair = 30;

	public int defaultDays = 10;

	public static DataValues instance { get; private set; }

	void Awake ()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void calculateTotals ()
	{
		totalHunger = 0;
		totalThirst = 0;
		totalMorale = 0;
		foreach (Pirate e in PirateManager.instance.getPirates()) {
			totalHunger += e.hunger;
			totalThirst += e.thirst;
			totalMorale += e.morale;
		}
	}

	public void setTotalHunger (int effect)
	{
		totalHunger += effect;
		Debug.Log ("Total Hunger: " + totalHunger);
	}
	
	public void setTotalThirst (int effect)
	{
		totalThirst += effect;
		Debug.Log ("Total Thirst: " + totalThirst);
	}
	
	public void setTotalMorale (int effect)
	{
		totalMorale += effect;
		Debug.Log ("Total Morale: " + totalMorale);
	}
	
	public int getMaxHunger ()
	{
		return maxHunger;
	}
	
	public int getMaxThirst ()
	{
		return maxThirst;
	}
	
	public int getMaxMorale ()
	{
		return maxMorale;
	}
}