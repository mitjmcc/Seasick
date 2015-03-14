using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour
{

	public Text[] textValues;

	//Should just move all the data values to this or a separate class.

	void Start ()
	{

	}

	void Update ()
	{
		updateText ();
	} 

	private void updateText ()
	{
		textValues [0].text = "Food: " + DataValues.instance.totalFood;
		textValues [1].text = "Water: " + DataValues.instance.totalWater;
		textValues [2].text = "Morale: " + DataValues.instance.totalMorale;
		textValues [3].text = "Days Left: " + (DataValues.instance.defaultDays - DayNightController.daysPast);
		textValues [4].text = "Wood: " + DataValues.instance.totalWood;
	}	
}