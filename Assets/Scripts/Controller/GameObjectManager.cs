using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectManager{
		
	public static GameObject CreateStone(Constants.StoneColor color, Vector2 position){		
		GameObject stone = safeInitialize(Constants.StonePrefabName, position);
		stone.GetComponent<StoneView> ().StoneColor = color;
		
		return stone;
	}

	/// <summary>
	/// Safely initializes a prefab
	/// </summary>
	/// <returns>The initialized GameObject.</returns>
	/// <param name="name">Name.</param>
	/// <param name="position">Position.</param>
	private static GameObject safeInitialize(string name, Vector3 position){
		return (GameObject)MonoBehaviour.Instantiate (Resources.Load<GameObject> (name), position, Quaternion.identity);
	}
}
