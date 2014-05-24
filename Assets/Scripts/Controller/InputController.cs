using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{

	public bool IsEventStart(){
		if (Input.mousePresent) {
			return Input.GetMouseButtonDown(0);
		} else {
			return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
		}
	}

	public bool IsEventEnd(){
		if (Input.mousePresent) {
			return Input.GetMouseButtonUp(0);
		} else {
			return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
		}
	}

	public Vector3 GetPosition ()
	{
		if (Input.mousePresent) {
			return Input.mousePosition;
		} else {
			if (Input.touchCount <= 0){
				return Vector2.zero;
			} else {
				return Input.GetTouch(0).position;
			}
		}
	}
}