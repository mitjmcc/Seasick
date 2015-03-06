using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject target;
	public float maxSpeed;
	public float maxVertSpeed;
	public float maxDistance;
	float horizAxis;
	float vertAxis;
	float mWheel;
	int zoomSpeed = 4;
	int speed = 200;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		horizAxis = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		vertAxis = Input.GetAxis ("Vertical") * Time.deltaTime * speed;

		mWheel = Input.GetAxis ("Mouse ScrollWheel");
		Vector3 vertTranslate = new Vector3 (0, vertAxis * maxVertSpeed, 0);
		Vector3 horizTranslate = new Vector3 (horizAxis * maxSpeed, 0, 0);
		transform.Translate (horizTranslate);
		Vector3 targetPos = target.transform.position;
		transform.Translate (vertTranslate);
		transform.LookAt (targetPos);
		if (mWheel > 0) {
			GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView - zoomSpeed;
			if (GetComponent<Camera>().fieldOfView <= 40) {
				GetComponent<Camera>().fieldOfView = 40;
			}
		} else if (mWheel < 0) {
			GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView + zoomSpeed;
			if (GetComponent<Camera>().fieldOfView >= 85) {
				GetComponent<Camera>().fieldOfView = 85;
			}
//			Camera.main.fieldOfView = Mathf.Max (Camera.main.fieldOfView + 2, 85.0);
		}
	}
}

