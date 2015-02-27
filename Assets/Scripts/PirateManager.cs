using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PirateManager : MonoBehaviour
{
		private static GameObject[] pirateObjects;

		public static int totalHunger = 0;
		public static int totalThirst = 0;
		public static int totalMorale = 0;
		public static ArrayList pirates;
		public static Pirate lastSelected;
		public Sprite[] pirateFaces = new Sprite[5];
		public GameObject selector;

		//Initialization
		void Awake ()
		{
				pirates = new ArrayList ();
				pirateObjects = GameObject.FindGameObjectsWithTag ("Pirates");
				for (int i = 0; i < pirateObjects.Length; i++)
						pirates.Add (pirateObjects [i].gameObject.GetComponent ("Pirate"));

				calculateTotals ();
		}

		void Update ()
		{
				checkForPirate ();
		}

		public static void calculateTotals ()
		{
				totalHunger = 0;
				totalThirst = 0;
				totalMorale = 0;
				foreach (Pirate e in pirates) {
						totalHunger += e.hunger;
						totalThirst += e.thirst;
						totalMorale += e.morale;
				}
		}

		public static Pirate getSelectedPirate ()
		{
				foreach (Pirate e in pirates) {
						if (e.selected)
								return e;
				}
				return null;
		}

		public static bool isAPirateSelected ()
		{
				foreach (Pirate e in pirates) {
						if (e.selected)
								return true;
				}
				return false;
		}

		public static void pirateJobReset ()
		{
				foreach (Pirate p in pirates) {
						//p.agent.SetDestination(p.origLocation);
						if (p.doneJob) {
								p.gameObject.transform.position = p.origLocation;
								p.agent.enabled = true;
								p.agent.SetDestination (p.origLocation);
								p.lastJob.gameObject.transform.position = p.lastJob.origLocation;
						}
						p.returning = true;
						p.doneJob = false;
				}
		}

		public void checkForPirate ()
		{
				if (Input.GetMouseButtonDown (0)) {
						foreach (Pirate e in pirates)
								e.selected = false;
						selector.renderer.enabled = false;
						Debug.Log ("Hit nothing");
						RaycastHit hitInfo = new RaycastHit ();
						bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
						if (hit) {
								Debug.Log ("Hit " + hitInfo.transform.gameObject.name);
								if (hitInfo.transform.gameObject.tag == "Pirates" && !hitInfo.transform.gameObject.GetComponent<Pirate> ().doneJob) {
										selector.transform.parent = null;
										selector.renderer.enabled = true;
										Pirate currentPirate = hitInfo.transform.gameObject.GetComponent<Pirate> ();
										currentPirate.selected = true;
										currentPirate.say (0);
										selector.transform.position = currentPirate.transform.position + new Vector3 (0, 5, 0);
										selector.transform.SetParent (currentPirate.transform, true);
										lastSelected = hitInfo.transform.gameObject.GetComponent<Pirate> ();
								}
						}
				}
		}

		public static void setHunger (int effect)
		{
				totalHunger += effect;
				Debug.Log ("Total Hunger: " + totalHunger);
		}

		public static void setThirst (int effect)
		{
				totalThirst += effect;
				Debug.Log ("Total Thirst: " + totalThirst);
		}

		public static void setMorale (int effect)
		{
				totalMorale += effect;
				Debug.Log ("Total Morale: " + totalMorale);
		}

		public static GameObject[] getPirateObjects ()
		{
				return pirateObjects;
		}
}