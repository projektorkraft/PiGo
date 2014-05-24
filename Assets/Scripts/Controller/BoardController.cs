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
		networkController.OnMessageReceived += (point) => {
			if (logicController.addStone (point)){
				var stoneColor = logicController.toPlay == Constants.StoneColor.Black ? Constants.StoneColor.White : Constants.StoneColor.Black;
				var stone = GameObjectManager.CreateStone (stoneColor, new Vector3((float)point.x, (float)point.y, 0));
			}
		};
	}

	// Update is called once per frame
	void Update () {

		if (guiController.IsMenu ()) return;

		var screenPos = Camera.main.ScreenToWorldPoint (inputController.GetPosition ());
		var worldPos = new Vector3 (screenPos.x, screenPos.y, -2);

		if (inputController.IsEventStart ()) {
			if (_stone == null) {
				_stone = GameObjectManager.CreateStone (logicController.toPlay, worldPos);
			}
		} else {
			if (_stone != null){
					_stone.transform.localPosition = worldPos;					
					_stone.GetComponent<StoneView>().Legal = logicController.HasPlace(new C2DPoint(worldPos.x, worldPos.y));				
				
				if (inputController.IsEventEnd ()) {
					var point = new C2DPoint(_stone.transform.position.x,
					                         _stone.transform.position.y);
					if (logicController.addStone(point)) {
						networkController.SendPoint(point);
						_stone.transform.localPosition = new Vector3(worldPos.x, worldPos.y, 0);
						_stone.GetComponent<StoneView>().HideBackground();
						_stone = null;
					} else {
						GameObject.Destroy(_stone);
					}
				}
			}
		}
	}	
}
