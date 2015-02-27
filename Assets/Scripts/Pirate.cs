using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pirate : MonoBehaviour
{

		private static int maxHunger = 20;
		private static int maxThirst = 15;
		private static int maxMorale = 50;

		public NavMeshAgent agent;

		public int hunger = 5;
		public int thirst = 5;
		public int morale = 5;

		public Vector3 origLocation;
		public bool selected = false;
		public bool doneJob = false;
		public bool returning = false;

		public Job lastJob;

		public AudioClip[] pirateSpeechClips;
	
		//Initialization
		void Start ()
		{
				agent = gameObject.GetComponent<NavMeshAgent> ();
				origLocation = gameObject.transform.position;
		}

		void Update ()
		{
				if (selected) {
						updateUI ();
				}

				if (gameObject.transform.position == origLocation)
						returning = false;
		}

		public void say (int audioIndex)
		{
				if (!audio.isPlaying) {
						audio.clip = pirateSpeechClips [audioIndex];
						audio.pitch = Random.Range (1.0F, 1.15F);
						audio.Play ();
				}
		}



		public static int getMaxHunger ()
		{
				return maxHunger;
		}

		public static int getMaxThirst ()
		{
				return maxThirst;
		}

		public static int getMaxMorale ()
		{
				return maxMorale;
		}

		public void updateUI ()
		{
				GameObject.Find ("StatBars").GetComponent<StatBarAnimator> ().changeHunger (hunger);
				GameObject.Find ("StatBars").GetComponent<StatBarAnimator> ().changeThirst (thirst);
				GameObject.Find ("StatBars").GetComponent<StatBarAnimator> ().changeMorale (morale);
				Sprite [] pirateFaces = GameObject.Find ("PirateManager").GetComponent<PirateManager> ().pirateFaces;
				GameObject.Find ("StatBars").GetComponent<StatBarAnimator> ().changeFaces (pirateFaces);
		}

		public void updateValues (bool h, bool t, bool m, int effect)
		{
				if (h)
						hunger += effect;
				Debug.Log (hunger);
				if (t)
						thirst += effect;
				Debug.Log (thirst);
				if (m)
						morale += effect;
				Debug.Log (morale);
		}
}