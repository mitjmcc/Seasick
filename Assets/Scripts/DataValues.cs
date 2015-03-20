using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class DataValues : MonoBehaviour
{
	public int totalHunger = 0;
	public int totalThirst = 0;
	public int totalMorale = 0;

	public int totalFood = 200;
	public int totalWater = 200;
	public int totalWood = 0;

	public float repair;
	
	public int maxHunger = 20;
	public int maxThirst = 15;
	public int maxMorale = 50;
	public int maxRepair = 30;

	public int defaultDays;

	public static DataValues instance { get; private set; }

	void Awake ()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void Save() 
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create 
			(Application.persistentDataPath + "/dataValues.dat");

		SeasickData data = new SeasickData ();

		data.curDays = DayNightController.daysPast;
		data.totalFood = totalFood;
		data.totalWater = totalWater;
		data.totalWood = totalWood;
		data.repair = repair;

		bf.Serialize (file, data);
		file.Close();

		int i = 1;		
//		foreach (Pirate p in PirateManager.instance.pirates) {
//			file = File.Create 
//				(Application.persistentDataPath + "/Pirates/pirate" + i + ".dat");
//
//			PirateData pData = new PirateData();
//
//			pData.hunger = p.hunger;
//			pData.morale = p.morale;
//			pData.name = p.name;
//			pData.curLocation = p.curLocation;
//			pData.thirst = p.thirst;
//
//			bf.Serialize(file, data);
//			file.Close();
//			i++;
//		}
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath +
			"/dataValues.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/dataValues.dat", 
				 FileMode.Open);
			SeasickData data = (SeasickData) bf.Deserialize(file);
			file.Close ();

			DayNightController.daysPast = data.curDays;
			totalFood = data.totalFood;
			totalWater = data.totalWater;
			totalWood = data.totalWood;
			repair = data.repair;
		}

//		int i = 1;
//		foreach (Pirate p in PirateManager.instance.pirates) {
//			if(File.Exists(Application.persistentDataPath + "/Pirates/pirate" + i + ".dat")) 
//			{
//				BinaryFormatter bf = new BinaryFormatter ();
//				FileStream file = File.Open 
//					(Application.persistentDataPath + "/Pirates/pirate" + i + ".dat",
//					FileMode.Open);
//				PirateData pData = (PirateData) bf.Deserialize(file);
//				file.Close();
//
//				p.hunger = pData.hunger;
//				p.morale = pData.morale;
//				p.name = pData.name;
//				p.transform.position = pData.curLocation;
//				p.thirst = pData.thirst;
//			}
//			i++;
//		}
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
//		Debug.Log ("Total Hunger: " + totalHunger);
	}
	
	public void setTotalThirst (int effect)
	{
		totalThirst += effect;
//		Debug.Log ("Total Thirst: " + totalThirst);
	}
	
	public void setTotalMorale (int effect)
	{
		totalMorale += effect;
//		Debug.Log ("Total Morale: " + totalMorale);
	}

	public void setFood (int effect)
	{
		totalFood += effect;
//		Debug.Log (totalFood);
	}
	
	public void setWater (int effect)
	{
		totalWater += effect;
//		Debug.Log (totalWater);
	}
	
	public void setWood (int effect)
	{
		totalWood += effect;
//		Debug.Log (totalWood);
	}

	public void setRepair (int effect) {
		repair += effect;
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

[Serializable]
class SeasickData
{
	public int totalFood;
	public int totalWater;
	public int totalWood;

	public float repair;

	public int curDays;

	public SeasickData() {

	}
}

[Serializable]
class PirateData 
{
	public int hunger;
	public int thirst;
	public int morale;	

	public String name;

	public Vector3 curLocation;
}