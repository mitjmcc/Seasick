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
		public int defaultDays = 30;

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
				foreach (Pirate e in PirateManager.pirates) {
						totalHunger += e.hunger;
						totalThirst += e.thirst;
						totalMorale += e.morale;
				}
		}
}