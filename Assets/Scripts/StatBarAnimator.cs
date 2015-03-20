using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatBarAnimator : MonoBehaviour
{
	private int hunger;
	private int thirst;
	private int morale;
	private int maxHunger;
	private int maxThirst;
	private int maxMorale;
	private float happiness; // causes background color of portrait to change. Ranges from -1 to 1;
	private Image portrait; 
	private ArrayList displays;
	public Sprite[] faces;

	// Use this for initialization
	void Start ()
	{
		maxHunger = DataValues.instance.getMaxHunger ();
		maxThirst = DataValues.instance.getMaxThirst ();
		maxMorale = DataValues.instance.getMaxMorale ();
		hunger = 0;
		thirst = 0;
		morale = 0;
		happiness = 1;
		displays = new ArrayList ();
		foreach (GameObject g in GameObject.FindGameObjectsWithTag ("CharacterDisplay")) {
			g.GetComponent<Image> ().enabled = false;
			displays.Add (g.GetComponent<Image> ());
		}
		portrait = GameObject.Find ("PirateFace").GetComponent<Image> ();
		portrait.enabled = false;
	}

	// Update is called once per frame
	void Update ()
	{
		updateBgColor ();

		if (!(PirateManager.instance.isAPirateSelected ())) {
			toggleCharacterGUI (false);
			portrait.enabled = false;
		} else {
			portrait.enabled = true;
			toggleCharacterGUI (true);
			updateBars();
			changePortrait ();
			//Debug.Log ("Happiness: " + happiness);
		}

		
	}

	private void updateBars() {
		if (hunger != PirateManager.instance.getSelectedPirate ().hunger)
			GameObject.Find ("HungerBar").GetComponent<Image> ().fillAmount = ((float) 
           (maxHunger - PirateManager.instance.getSelectedPirate ().hunger) / maxHunger);
		if (thirst != PirateManager.instance.getSelectedPirate ().thirst)
			GameObject.Find ("ThirstBar").GetComponent<Image> ().fillAmount = ((float) 
           (maxThirst - PirateManager.instance.getSelectedPirate ().thirst) / maxThirst);
		if (morale != PirateManager.instance.getSelectedPirate ().morale)
			GameObject.Find ("MoraleBar").GetComponent<Image> ().fillAmount = ((float) 
           (maxMorale - PirateManager.instance.getSelectedPirate ().morale) / maxMorale);
	}

	private void updateBgColor ()
	{
		happiness = 2 * ((((float)(hunger / maxHunger) + ((float)(thirst) / maxThirst)
		                  + ((float)(morale) / maxMorale)) / 3) - 1); 
		//taking average of percentages, multiplying by 2, subtracting 1. Should give a range from -1 to 1.

		if (happiness > 0) {
			GameObject.Find ("FaceBackground").GetComponent<Image> ().color = new Color (1F - happiness, 1F, 0F);
		} else if (happiness == -1) {
			GameObject.Find ("FaceBackground").GetComponent<Image> ().color = new Color (1F, 1F, 1F);
		} else {
			GameObject.Find ("FaceBackground").GetComponent<Image> ().color = new Color (1F, 1F + happiness, 0F);
		}
	}

	public void changeHunger (int amount)
	{
		hunger = amount;
	}

	public void changeThirst (int amount)
	{
		thirst = amount;
	}

	public void changeMorale (int amount)
	{
		morale = amount;
	}

	public void changeFaces (Sprite[] pirateFaces)
	{
		faces = pirateFaces;

	}

	public void changePortrait ()
	{
			if (happiness > 0.6F) {
					portrait.sprite = faces [0];
			} else if (happiness <= 0.6F && happiness > 0.2F) {
					portrait.sprite = faces [1];
			} else if (happiness <= 0.2F && happiness > -0.2F) {
					portrait.sprite = faces [2];
			} else if (happiness <= -0.2F && happiness > -0.6F) {
					portrait.sprite = faces [3];
			} else {
//						portrait.sprite = faces [4];
			}

	}

	public void toggleCharacterGUI (bool b)
	{
		foreach (Image i in displays)
			i.enabled = b;
	}
}
