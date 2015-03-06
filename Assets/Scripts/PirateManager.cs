using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PirateManager : MonoBehaviour
{
		private GameObject[] pirateObjects;

		public Pirate[] pirates;
		public Pirate lastSelected;
		public Sprite[] pirateFaces = new Sprite[5];
		public GameObject selector;

		public static PirateManager instance { get; private set; }
		
		//Initialization
		void Awake ()
		{
			instance = this;

			pirateObjects = GameObject.FindGameObjectsWithTag ("Pirates");

			DataValues.instance.calculateTotals ();
		}

		void Update ()
		{
			checkForPirate ();
		}

		public Pirate getSelectedPirate ()
		{
			foreach (Pirate e in pirates) {
				if (e.selected)
					return e;
			}
			return null;
		}

		public bool isAPirateSelected ()
		{
			foreach (Pirate e in pirates) {
				if (e.selected)
					return true;
			}
			return false;
		}

		public void pirateJobReset ()
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

	public void pirateJobReset (Pirate p)
	{
		//p.agent.SetDestination(p.origLocation);
		if (p.doneJob) {
			if (p.lastJob.boatJob) 
				p.gameObject.transform.position = p.origLocation;
			p.agent.enabled = true;
			p.lastJob.gameObject.transform.position = p.lastJob.origLocation;
		}
		p.anim.SetBool ("walk", false);
		p.agent.SetDestination (p.origLocation);
		p.returning = true;
		p.doneJob = false;
	}

		public void checkForPirate ()
		{
				if (Input.GetMouseButtonDown (0)) {
						foreach (Pirate e in pirates)
								e.selected = false;
						selector.GetComponent<Renderer>().enabled = false;
						Debug.Log ("Hit nothing");
						RaycastHit hitInfo = new RaycastHit ();
						bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
						if (hit) {
								Debug.Log ("Hit " + hitInfo.transform.gameObject.name);
								if (hitInfo.transform.gameObject.tag == "Pirates" && !hitInfo.transform.gameObject.GetComponent<Pirate> ().doneJob) {
										selector.transform.parent = null;
										selector.GetComponent<Renderer>().enabled = true;
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

		public GameObject[] getPirateObjects ()
		{
			return pirateObjects;
		}

		public Pirate[] getPirates() {
			return pirates;		
		}
}