using UnityEngine;
using System.Collections;

public class StoneView : MonoBehaviour {
	
	public Renderer stoneRenderer;
	public GameObject backGround;

	private Constants.StoneColor _color;

	private bool _legal;
	[HideInInspector]
	public bool Legal{
		set {
			if (_legal != value){
				_legal = value;
				SetBackgroundColor (_legal ? Color.green : Color.red);
			}
		}
	}

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
		SetBackgroundColor (Color.green);
	}

	private void SetBackgroundColor(Color color){
		var nColor = new Color (color.r, color.g, color.b, 0.5f);
		backGround.GetComponent<MeshRenderer> ().material.color = nColor;
	}

	public void HideBackground(){
		backGround.SetActive (false);
	}
	
	private void updateColor(){
		stoneRenderer.material.color = _color == Constants.StoneColor.Black ? Color.grey : Color.white;
	}
}
