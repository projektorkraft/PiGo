using UnityEngine;
using System.Collections;
using GeoLib;
using System.Collections.Generic;

public class LogicController : MonoBehaviour {

	List<C2DCircle> forbiddenShapes;
	List<C2DPolyArc> blackShape;
	List<C2DPolyArc> whiteShape;

	public Constants.StoneColor toPlay;

	// Use this for initialization
	void Start () {
		forbiddenShapes = new List<C2DCircle> ();
		blackShape = new List<C2DPolyArc>();
		whiteShape = new List<C2DPolyArc>();
		toPlay = Constants.StoneColor.Black;
	}

	public bool addStone(C2DPoint pos) {

		Constants.StoneColor color = toPlay;
		//TODO: copy here
		List<C2DPolyArc> _blackShape = blackShape;
		List<C2DPolyArc> _whiteShape = whiteShape;
		if (hasPlace (pos) && isLegal(pos, color, _blackShape, _whiteShape)) {
			makeMove (pos, color, blackShape, whiteShape);
			afterMove (pos, color);
			return true;
		}
		else return false;
	}

	void makeMove (C2DPoint pos, Constants.StoneColor color, List<C2DPolyArc> blackShape, List<C2DPolyArc> whiteShape)
	{
		//TODO:implement
	}

 /*
	void makeMove (C2DPoint pos, Constants.StoneColor color, List<C2DPolyArc> blackShape, List<C2DPolyArc> whiteShape)
	{
		C2DPolyArc stoneShape = new C2DPolyArc(new C2DCircle (pos, 1));

		List<C2DPolyArc> ownShape;
		if (color == Constants.StoneColor.Black) {
			ownShape = blackShape;
		} else {
			ownShape = whiteShape;
		}

		List<C2DPolyArc> shapesToMerge = new List<C2DPolyArc> ();
		foreach (C2DPolyArc poly in ownShape) {
			if (poly.Overlaps(stoneShape)) {
				shapesToMerge.Add(poly);
			}
		}

		merge(stoneShape, shapesToMerge, ownShape);

	}

	void merge(C2DPolyArc stoneShape, List<C2DPolyArc> shapesToMerge, List<C2DPolyArc> ownShape) {

		Debug.Log (shapesToMerge [0].GetArea ());

		C2DPolyArc stone = stoneShape;

		foreach (C2DPolyArc shape in shapesToMerge) {
			shapesToMerge.Remove(shape);
		}

		foreach (C2DPolyArc shape in shapesToMerge) {
			shape.GetUnion(stone, shapesToMerge, new CGrid());
		}

		Debug.Log (shapesToMerge [0].GetArea ());
	}
*/
	void afterMove (C2DPoint pos, Constants.StoneColor color)
	{
		forbiddenShapes.Add (new C2DCircle (pos, Constants.stoneSize));
		if (color == Constants.StoneColor.Black) {
			toPlay = Constants.StoneColor.White;
		}
		else {
			toPlay = Constants.StoneColor.Black;
		}
	}

	bool hasPlace(C2DPoint pos) {
		Debug.Log("checking " + pos.x + " : " + pos.y);
		foreach (C2DCircle circle in forbiddenShapes) {
			Debug.Log("against " + circle.Centre.x + " : " + circle.Centre.y);
			if (circle.Contains(pos)) {
				Debug.Log("false");
				return false;
			} else {
				Debug.Log("true");
			}
		}
		return true;
	}

	bool isLegal (C2DPoint pos, Constants.StoneColor color, List<C2DPolyArc> _blackShape, List<C2DPolyArc> _whiteShape)
	{
		return true;
	}
}
