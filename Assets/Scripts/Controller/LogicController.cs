using UnityEngine;
using System.Collections;
using GeoLib;
using System.Collections.Generic;
using System.Linq;

public class LogicController : MonoBehaviour {

	List<C2DCircle> forbiddenShapes;

	public Constants.StoneColor toPlay;

	// Use this for initialization
	void Start () {
		forbiddenShapes = new List<C2DCircle> ();
		toPlay = Constants.StoneColor.Black;
	}

	public bool addStone(C2DPoint pos) {

		Constants.StoneColor color = toPlay;

		if (HasPlace (pos) && isLegal(pos, color)) {
			makeMove (pos, color);
			return true;
		}
		else return false;
	}

	void makeMove (C2DPoint pos, Constants.StoneColor color)
	{
		forbiddenShapes.Add (new C2DCircle (pos, Constants.StoneSize));

		if (color == Constants.StoneColor.Black) {
			toPlay = Constants.StoneColor.White;
		} else {
			toPlay = Constants.StoneColor.Black;
		}
	}

	bool isLegal(C2DPoint pos, Constants.StoneColor color) {
		//TODO: implement
		return true;
	}

	public bool HasPlace(C2DPoint pos) {
		return !forbiddenShapes.Any ((shape) => shape.Contains (pos));
	}

}
