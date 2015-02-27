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
				textValues [0].text = "Food: " + JobManager.totalFood;
				textValues [1].text = "Water: " + JobManager.totalWater;
				textValues [2].text = "Morale: " + PirateManager.totalMorale;
				textValues [3].text = "Days Left: " + (JobManager.defaultDays - DayNightController.daysPast);
				textValues [4].text = "Wood: " + JobManager.totalWood;
		}	
}