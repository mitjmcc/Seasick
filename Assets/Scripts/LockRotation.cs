using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour {

	public GameObject target;
	public Vector3 buffer;
	public Quaternion rotate;
	public bool enabled;
	public Transform parent;

	//This is quickly evolving into just an icon class but I may not change the name of the class

	void Start () {
		gameObject.SetActive (false);
		rotate = transform.rotation;
	}

	void Update() {
		transform.rotation = rotate;
		transform.position = parent.position + buffer;
	}

	public void SetTarget(GameObject g){
		target = g;
	}

	public void SetParentTransform(Transform t) {
		parent = t;
	}
}