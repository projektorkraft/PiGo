using UnityEngine;
using System.Collections;
using GeoLib;
using System.Collections.Generic;
using System.Linq;

public class LogicController : MonoBehaviour {

	List<C2DHoledPolyArc> forbiddenShapes;
	List<C2DHoledPolyArc> blackShape;
	List<C2DHoledPolyArc> whiteShape;

	public Constants.StoneColor toPlay;

	// Use this for initialization
	void Start () {
		forbiddenShapes = new List<C2DHoledPolyArc> ();
		blackShape = new List<C2DHoledPolyArc>();
		whiteShape = new List<C2DHoledPolyArc>();
		toPlay = Constants.StoneColor.Black;
	}

	void Update(){

		foreach (var fshape in forbiddenShapes) {
			foreach(var line in fshape.Rim.Lines){
				Vector2 from = new Vector2((float)line.GetPointFrom().x, (float)line.GetPointFrom().y);
				Vector2 to = new Vector2((float)line.GetPointTo().x, (float)line.GetPointTo().y);

				Debug.DrawLine(from,to, Color.red);
			}

		}
	}

	public bool addStone(C2DPoint pos) {

		Constants.StoneColor color = toPlay;
		//TODO: copy here
		List<C2DHoledPolyArc> _blackShape = blackShape;
		List<C2DHoledPolyArc> _whiteShape = whiteShape;
		if (HasPlace (pos) && isLegal(pos, color, _blackShape, _whiteShape)) {
			makeMove (pos, color, blackShape, whiteShape);
			afterMove (pos, color);
			return true;
		}
		return false;
	}

	C2DHoledPolyArc makeCircle (C2DPoint pos, float radius) {
		C2DPolyArc shape = new C2DPolyArc ();
		
		shape.SetStartPoint (new C2DPoint(pos.x+radius,pos.y));

		for (int i = 0; i < 16; i++) {
			shape.LineTo(new C2DPoint (pos.x + Mathf.Cos(Mathf.PI*2*i/16)*radius, pos.y+ Mathf.Sin(Mathf.PI*2*i/16)*radius), radius, false, true);
		}
		shape.Close (radius, false, true);

		C2DHoledPolyArc result = new C2DHoledPolyArc ();
		result.Rim = shape;
		return result;
	}

 
	void makeMove (C2DPoint pos, Constants.StoneColor color, List<C2DHoledPolyArc> blackShape, List<C2DHoledPolyArc> whiteShape)
	{
		C2DHoledPolyArc stoneShape = makeCircle (pos, 1);
		
		List<C2DHoledPolyArc> ownShape;
		if (color == Constants.StoneColor.Black) {
			ownShape = blackShape;
		} else {
			ownShape = whiteShape;
		}
		
		List<C2DHoledPolyArc> shapesToMerge = new List<C2DHoledPolyArc> ();
		foreach (C2DHoledPolyArc poly in ownShape) {
			if (poly.Overlaps(stoneShape)) {
				shapesToMerge.Add(poly);
			}
		}
		
		merge(stoneShape, shapesToMerge, ownShape);
		
	}
	
	void merge(C2DHoledPolyArc stoneShape, List<C2DHoledPolyArc> shapesToMerge, List<C2DHoledPolyArc> ownShape) {
		
		List<C2DHoledPolyArc> acc = new List<C2DHoledPolyArc>();
		CGrid grid = new CGrid ();
		grid.SetGridSize(0.01f);
		
		if (shapesToMerge.Count == 0) {
			ownShape.Add(stoneShape);
			return;
		}
		
		foreach (C2DHoledPolyArc shape in shapesToMerge) {
			ownShape.Remove(shape);
		}
		
		foreach (C2DHoledPolyArc shape in shapesToMerge) {
			shape.GetUnion(stoneShape, acc, grid);
		}
		
		ownShape.Add(acc[acc.Count-1]);
		
	}
	
	void afterMove (C2DPoint pos, Constants.StoneColor color)
	{
		forbiddenShapes.Add (makeCircle(pos,Constants.stoneSize));
		if (color == Constants.StoneColor.Black) {
			toPlay = Constants.StoneColor.White;
		}
		else {
			toPlay = Constants.StoneColor.Black;
		}
		Debug.Log (blackShape.Count);
		Debug.Log (whiteShape.Count);
	}

	public bool HasPlace(C2DPoint pos) {
		return !forbiddenShapes.Any ((shape) => shape.Contains (pos));
	}

	bool isLegal (C2DPoint pos, Constants.StoneColor color, List<C2DHoledPolyArc> _blackShape, List<C2DHoledPolyArc> _whiteShape)
	{
		return true;
	}
}
