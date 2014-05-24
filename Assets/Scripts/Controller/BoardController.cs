using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {

	public InputController inputController;

	private GameObject _stone;

	// Update is called once per frame
	void Update () {

		var screenPos = Camera.main.ScreenToWorldPoint (inputController.GetPosition ());
		var worldPos = new Vector3 (screenPos.x, screenPos.y, 0);

		if (inputController.IsEventStart ()) {
			if (_stone == null) {
				_stone = GameObjectManager.CreateStone (Constants.StoneColor.White, worldPos);
			}
		} else {
			if (_stone != null){
				_stone.transform.localPosition = worldPos;
			}
			if (inputController.IsEventEnd ()) {		
				_stone = null;
			}
		}
	}	
}
