using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NightlyEvent : MonoBehaviour {
	
	public int effect;
	private int baseEffect;
	public int maxVariation;

	public Text description;
	
	public bool affectsFood = false;
	public bool affectsWater = false;
	public bool affectsWood = false;
	public bool affectsMorale = false;
	public bool variation = false;
	
	void Start () {
		description.enabled = false;
		baseEffect = effect;
		if (variation)
			variate ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void doAffect() {
		if (affectsFood) {
			DataValues.instance.setFood(effect);
			DataValues.instance.setTotalHunger(effect);
		}
		if (affectsWater) {
			DataValues.instance.setWater(effect);
			DataValues.instance.setTotalThirst(effect);
		}
		if (affectsWood) {
			DataValues.instance.setWood (effect);
			DataValues.instance.calculateTotals ();
		}
	}

	private void variate() {
		effect += Random.Range (-maxVariation, maxVariation);
		description.text = description.text.Replace ("" + baseEffect, "" + effect);
	}
}