using UnityEngine;
using System.Collections;
using GeoLib;

public class BoardController : MonoBehaviour {

	public InputController inputController;
	public LogicController logicController;

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
				if (logicController.addStone(new C2DPoint(transform.position.x,
				                                          transform.position.y))) {

				} else {
					GameObject.Destroy(_stone);
				}
				
				_stone = null;
			}
		}
	}	
}
