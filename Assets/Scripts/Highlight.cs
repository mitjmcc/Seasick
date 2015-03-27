using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {

	private static Color startcolor;
	public Renderer rend;
	public Color hl;

	// Use this for initialization
	void Start () {
		startcolor = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("space"))
			rend.material.color = hl;
		if (Input.GetKeyUp("space"))
			rend.material.color = startcolor;
	}


	void OnMouseEnter()
	{
		rend.material.color = hl;
	}
	void OnMouseExit()
	{
		rend.material.color = startcolor;
	}
}
