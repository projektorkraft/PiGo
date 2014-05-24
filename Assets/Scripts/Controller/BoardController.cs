using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObjectManager.CreateStone (Constants.StoneColor.Black, new Vector2 (1, 1));
	}
	
	// Update is called once per frame
	void Update () {

		/*
		if (Input.GetTouch(1).) {
			
		}*/
	}
}
