using UnityEngine;
using System.Collections;

public class StoneView : MonoBehaviour {

	public Renderer backgroundRenderer;

	// Use this for initialization
	void Start () {
		backgroundRenderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
