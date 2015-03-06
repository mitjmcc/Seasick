using UnityEngine;
using System.Collections;

public class AddNavigation : MonoBehaviour {
	
	public Vector3 mouseLocation;
	public Transform trans;

	private NavMeshAgent agent;

	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
		mouseLocation = Input.mousePosition;
	}

	void Update () {


		//Locate Cursor
		mouseLocation = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay(mouseLocation);
		Plane playerPlane = new Plane (Vector3.up, transform.position);
		//Move to target Position
		if (Input.GetMouseButtonDown(1) && gameObject.gameObject.GetComponent<Pirate>().selected)
		{
			float hitDist = 0.0f;
			playerPlane.Raycast(ray, out hitDist);
			Vector3 targetPoint = ray.GetPoint(hitDist);
			agent.enabled = true;
			agent.updatePosition = true;
			agent.updateRotation = true;
			agent.SetDestination(targetPoint);
			gameObject.gameObject.GetComponent<Pirate>().say (1);

		}
	}
}