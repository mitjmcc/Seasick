using UnityEngine;
using UnityEditor;

public class AddSelectedAnimationsToRig : ScriptableWizard
{
	public GameObject rig;
	
	[MenuItem ("Custom/Add selected animations to rig")]
	static void DoSet()
	{
		ScriptableWizard.DisplayWizard("Add selected animations to rig", typeof(AddSelectedAnimationsToRig), "Set animations..."); 
	}
	
	void OnWizardUpdate()
	{	    
		if(!rig){
			// Disable the wizard create button
			isValid = false;
			
			// And tell the user why
			helpString = "Choose a rig with an animation component already attached to it.";
		} else {
			isValid = true;
			helpString = "Add selected animations to the rig...";
		} 
	}
	
	void OnWizardCreate()
	{
		Object[] objs = Selection.GetFiltered(typeof(Animation), SelectionMode.Unfiltered);
		
		foreach (Animation an in objs)
		{			
			rig.GetComponent<Animation>().AddClip(an.clip,an.clip.name);        
		}
		
		Selection.activeObject=rig;
		
		// bit of clean up
		objs=null;
		rig=null;   
	}   
}