using UnityEngine;
using System.Collections;
using GeoLib;

public class LogicController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		C2DPointSet pts = new C2DPointSet();

		pts.AddCopy (new C2DPoint(1.0f, 1.0f));
		pts.AddCopy (new C2DPoint(2.0f, 2.0f));
		pts.AddCopy (new C2DPoint(1.0f, 2.0f));
		pts.AddCopy (new C2DPoint(2.0f, 1.0f));

		C2DPolygon Poly1 = new C2DPolygon(pts,true);
		bool bTest = Poly1.HasCrossingLines();

		Debug.Log (bTest);

	}

}
