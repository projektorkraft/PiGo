using UnityEngine;
using System.Collections;
using GeoLib;

public class BoardController : MonoBehaviour {

	public InputController inputController;
	public LogicController logicController;
	public GuiController guiController;
	public NetworkController networkController;
	public GameObject board;

	private GameObject _stone;
	private Rect _boardBounds;

	void Awake(){
		//Actions for the menu
		guiController.BeServerAction += networkController.BeServer;
		guiController.ConnectServerAction += networkController.ConnectToServer;

		//Actions for networking i.e. set the stone;
		networkController.OnMessageReceived += (point) => {
			if (logicController.addStone (point)){
				var stoneColor = logicController.toPlay == Constants.StoneColor.Black ? Constants.StoneColor.White : Constants.StoneColor.Black;
				var stone = GameObjectManager.CreateStone (stoneColor, new Vector3((float)point.x, (float)point.y, 0));
			}
		};
		var globalScale = GetWorldScale (board.transform);
		var xHalf = globalScale.x / 2;
		var yHalf = globalScale.y / 2;
		_boardBounds = new Rect (-xHalf,-yHalf , globalScale.x, globalScale.y);
	}

	// Update is called once per frame
	void Update () {
		
		if (guiController.IsMenu ()) return;

		var screenPos = Camera.main.ScreenToWorldPoint (inputController.GetPosition ());
		var worldPos = new Vector3 (screenPos.x, screenPos.y, -2);

		if (inputController.IsEventStart () && IsInBounds(worldPos)) {
			if (_stone == null) {
				_stone = GameObjectManager.CreateStone (logicController.toPlay, worldPos);
			}
		} else {
			if (_stone != null){
					_stone.transform.localPosition = worldPos;					
					_stone.GetComponent<StoneView>().Legal = logicController.HasPlace(new C2DPoint(worldPos.x, worldPos.y)) && IsInBounds(worldPos);
				
				if (inputController.IsEventEnd ()) {
					var point = new C2DPoint(_stone.transform.position.x,
					                         _stone.transform.position.y);
					if (logicController.addStone(point) && IsInBounds(worldPos)) {
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

	private Vector3 GetWorldScale(Transform transform)	
	{
		Vector3 worldScale = transform.localScale;	
		Transform parent = transform.parent;
		
		while (parent != null){
			worldScale = Vector3.Scale(worldScale,parent.localScale);
			parent = parent.parent;
		}		
		
		return worldScale;
	}

	bool IsInBounds (Vector2 worldPos)
	{
		return worldPos.x - Constants.StoneSize/2 >= _boardBounds.x &&
				worldPos.y - Constants.StoneSize/2 >= _boardBounds.y &&
				worldPos.x + Constants.StoneSize/2 <= _boardBounds.x + _boardBounds.width &&
				worldPos.y + Constants.StoneSize/2 <= _boardBounds.y + _boardBounds.height;
	}
}
