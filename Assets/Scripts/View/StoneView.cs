using UnityEngine;
using System.Collections;

public class StoneView : MonoBehaviour {

	public Renderer backgroundRenderer;
	public Renderer stoneRenderer;

	private Constants.StoneColor _color;

	[HideInInspector]
	public Constants.StoneColor StoneColor { 
		get {return _color;} 
		set {
			_color = value;
			updateColor();
			}
		}

	// Use this for initialization
	void Start () {
		backgroundRenderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}	

	private void updateColor(){

		stoneRenderer.material.color = _color == Constants.StoneColor.Black ? Color.black : Color.white;
	}
}
