using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour
{

	public Text[] textValues;
	public Image[] bars;

	//Should just move all the data values to this or a separate class.

	void Start ()
	{

	}

	void Update ()
	{
		updateText ();
		updateBars ();
	} 

	private void updateText ()
	{
		textValues [0].text = "Food: " + DataValues.instance.totalFood;
		textValues [1].text = "Water: " + DataValues.instance.totalWater;
		textValues [2].text = "Morale: " + DataValues.instance.totalMorale;
		textValues [3].text = "Days Left: " + (DataValues.instance.defaultDays - DayNightController.daysPast);
		textValues [4].text = "Wood: " + DataValues.instance.totalWood;
		textValues[5].text = "Repair: " + DataValues.instance.repair / DataValues.instance.maxRepair;
	}

	private void updateBars() {
		bars [0].fillAmount = DataValues.instance.totalFood / DataValues.instance.maxFood;
		bars [1].fillAmount = DataValues.instance.totalWater / DataValues.instance.maxWater;
		bars [2].fillAmount = DataValues.instance.totalWood / DataValues.instance.maxWood;
		bars [3].fillAmount = DataValues.instance.totalMorale / DataValues.instance.maxMorale;
		bars [4].fillAmount = DataValues.instance.repair / DataValues.instance.maxRepair;
	}
}