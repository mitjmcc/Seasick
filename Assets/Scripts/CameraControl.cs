using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject target;
	public float maxSpeed;
	public float maxVertSpeed;
	public float maxDistance;
	public float rotateSpeed = 20.0f;
	public float angleMax = 30.0f;
	public bool mouseControl;
	public Transform t;
	float horizAxis;
	float vertAxis;
	float mWheel;
	int zoomSpeed = 4;
	int speed = 200;
	int mDelta = 10;
	Vector3 targetPos;
	Vector3 currentVector;
	Vector3 initialVector;

	void Start () {
		initialVector = transform.position - target.transform.position;
		initialVector.y = 0; 
		targetPos = target.transform.position;
		transform.LookAt(targetPos);

	}
	
	// Update is called once per frame
	void Update () {
		if (!DayNightController.DayPhase.Night.Equals(DayNightController.GetCurrentPhase())) {
			if ((Input.mousePosition.x >= Screen.width - mDelta) && mouseControl)
				horizAxis = Time.deltaTime * speed;
			else if (Input.mousePosition.x <= mDelta && mouseControl)
				horizAxis = -1 * Time.deltaTime * speed;
			else
				horizAxis = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;

			if ((Input.mousePosition.y >= Screen.height - mDelta)&& mouseControl)
				vertAxis = Time.deltaTime * speed;
			else if (Input.mousePosition.y <= mDelta&& mouseControl)
				vertAxis = -1 * Time.deltaTime * speed;
			else
				vertAxis = Input.GetAxis ("Vertical") * Time.deltaTime * speed;

			mWheel = Input.GetAxis ("Mouse ScrollWheel");

			//Vector3 vertTranslate = new Vector3 (0, vertAxis * maxVertSpeed, 0);	
			Vector3 horizTranslate = new Vector3 (horizAxis * maxSpeed, 0, 0);

			transform.Translate (horizTranslate);
			//transform.Translate (vertTranslate);

			currentVector = transform.position - target.transform.position;
			currentVector.y = 0;
			float rotateDegrees = vertAxis * maxVertSpeed;
			float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).y > 0 ? 1 : -1);            
			float newAngle /*Mathf.Clamp(angleBetween + rotateDegrees, -angleMax, angleMax)*/ = 0;

			if (angleBetween + rotateDegrees >= angleMax)
				newAngle = angleMax;
			else if (angleBetween + rotateDegrees <= -angleMax)
				newAngle = -angleMax;
			else
				newAngle = angleBetween + rotateDegrees;

			rotateDegrees = newAngle - angleBetween;
			transform.RotateAround(target.transform.position, Vector3.back, rotateDegrees);
		}
		CameraBounds ();
	}

	void CameraMovement() {

	}

	void CameraBounds() {
		if (t.position.z >= 100)
			transform.position = new Vector3 (t.position.x, t.position.y, 100);
		else if (t.position.z <= -40)
			transform.position = new Vector3 (t.position.x, t.position.y, -40);
		//if (t.rotation.z <= -.4f)
			//transform.rotation = new Quaternion (t.rotation.w, t.rotation.x, t.rotation.y, -.4f);

		if (mWheel > 0) {
			GetComponent<Camera> ().fieldOfView = GetComponent<Camera> ().fieldOfView - zoomSpeed;
			if (GetComponent<Camera> ().fieldOfView <= 40) {
				GetComponent<Camera> ().fieldOfView = 40;
			}
		} else if (mWheel < 0) {
			GetComponent<Camera> ().fieldOfView = GetComponent<Camera> ().fieldOfView + zoomSpeed;
			if (GetComponent<Camera> ().fieldOfView >= 85) {
				GetComponent<Camera> ().fieldOfView = 85;
			}
		}
	}
}

