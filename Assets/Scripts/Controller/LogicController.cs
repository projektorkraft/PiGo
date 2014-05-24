using UnityEngine;
using System.Collections;
using GeoLib;
using System.Collections.Generic;

public class LogicController : MonoBehaviour {

	List<C2DCircle> forbiddenShapes;

	// Use this for initialization
	void Start () {

	}

	bool addStone(C2DPoint pos, Constants.StoneColor color) {
		if (hasPlace (pos) && isLegal(pos, color)) {
			addShape (pos);
			return true;
		}
		else return false;
	}

	void addShape (C2DCircle pos)
	{
		forbiddenShapes.Add (C2DCircle (C2DPoint, Constants.stoneSize));
	}

	bool isLegal(C2DPoint pos, Constants.StoneColor color) {
		//TODO: implement
	}

	bool hasPlace(C2DPoint pos) {
		foreach (C2DCircle circle in forbiddenShapes) {
			if (circle.Contains(pos)) {
				return false;
			}
		}
		return true;
	}

}
