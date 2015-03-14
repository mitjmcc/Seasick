using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {

	private static Color startcolor;
	public Renderer rend;
	public Color hl;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseEnter()
	{

		startcolor = rend.material.color;
		rend.material.color = hl;
	}
	void OnMouseExit()
	{
		rend.material.color = startcolor;
	}
}
