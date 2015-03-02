using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

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
	private Sprite[] faces;

	// Use this for initialization
	void Start ()
	{
		maxHunger = DataValues.instance.getMaxHunger ();
		maxThirst = DataValues.instance.getMaxThirst ();
		maxMorale = DataValues.instance.getMaxMorale ();
		hunger = 0;
		thirst = 0;
		morale = 0;
		happiness = -1;
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
			hunger = 0;
			thirst = 0;
			morale = 0;
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
		GameObject.Find ("HungerBar").GetComponent<Image> ().fillAmount = ((float) 
           PirateManager.instance.getSelectedPirate ().hunger / maxHunger);
		GameObject.Find ("ThirstBar").GetComponent<Image> ().fillAmount = ((float) 
           PirateManager.instance.getSelectedPirate ().thirst / maxThirst);
		GameObject.Find ("MoraleBar").GetComponent<Image> ().fillAmount = ((float) 
           PirateManager.instance.getSelectedPirate ().morale / maxMorale);
	}

	private void updateBgColor ()
	{
		happiness = 2 * ((((float)hunger / maxHunger) + ((float)thirst / maxThirst) + ((float)morale / maxMorale)) / 3) - 1; 
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
		hunger = (amount < 0) ? 0 : ((amount > maxHunger) ? maxHunger : amount);
	}

	public void changeThirst (int amount)
	{
		thirst = (amount < 0) ? 0 : ((amount > maxThirst) ? maxThirst : amount);
	}

	public void changeMorale (int amount)
	{
		morale = (amount < 0) ? 0 : ((amount > maxMorale) ? maxMorale : amount);
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
