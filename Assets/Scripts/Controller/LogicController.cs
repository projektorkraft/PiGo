using UnityEngine;
using System.Collections;
using GeoLib;
using System.Collections.Generic;
using System.Linq;

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
			
		if (HasPlace (pos) && isLegal(pos, color, _blackShape, _whiteShape)) {
			makeMove (pos, color, blackShape, whiteShape);
			afterMove (pos, color);
			return true;
		}

		return false;
	}

	void makeMove (C2DPoint pos, Constants.StoneColor color, List<C2DPolyArc> blackShape, List<C2DPolyArc> whiteShape)
	{
		forbiddenShapes.Add (new C2DCircle (pos, Constants.StoneSize));
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
		forbiddenShapes.Add (new C2DCircle (pos, Constants.StoneSize));
		if (color == Constants.StoneColor.Black) {
			toPlay = Constants.StoneColor.White;
		}
		else {
			toPlay = Constants.StoneColor.Black;
		}
	}

	public bool HasPlace(C2DPoint pos) {
		return !forbiddenShapes.Any ((shape) => shape.Contains (pos));
	}

	bool isLegal (C2DPoint pos, Constants.StoneColor color, List<C2DPolyArc> _blackShape, List<C2DPolyArc> _whiteShape)
	{
		return true;
	}
}
