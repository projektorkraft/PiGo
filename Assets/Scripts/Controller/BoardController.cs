using UnityEngine;
using System.Collections;
using GeoLib;

public class BoardController : MonoBehaviour {

	public InputController inputController;
	public LogicController logicController;
	public GuiController guiController;
	public NetworkController networkController;

	private GameObject _stone;

	void Awake(){
		guiController.BeServerAction += networkController.BeServer;
		guiController.ConnectServerAction += networkController.ConnectToServer;
	}

	// Update is called once per frame
	void Update () {

		if (guiController.IsMenu ()) return;

		var screenPos = Camera.main.ScreenToWorldPoint (inputController.GetPosition ());
		var worldPos = new Vector3 (screenPos.x, screenPos.y, 0);

		if (inputController.IsEventStart ()) {
			if (_stone == null) {
				_stone = GameObjectManager.CreateStone (Constants.StoneColor.White, worldPos);
				_stone.GetComponent<StoneView>().StoneColor = logicController.toPlay;
			}
		} else {
			if (_stone != null){
				_stone.transform.localPosition = worldPos;
			}
			if (inputController.IsEventEnd ()) {
				if (logicController.addStone(new C2DPoint(_stone.transform.position.x,
				                                          _stone.transform.position.y))) {

				} else {
					GameObject.Destroy(_stone);
				}
				
				_stone = null;
			}
		}
	}	
}
