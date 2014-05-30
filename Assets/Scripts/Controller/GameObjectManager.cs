using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectManager{

	private static Material _material;

	private static Material GetMaterial(){
		if (_material == null) {
			_material = Resources.Load("Materials/StoneMaterial", typeof(Material)) as Material;
		}
		return _material;
	}

	public static GameObject CreateStone(Constants.StoneColor color, Vector3 position){
		GameObject stone = safeInitialize(Constants.StonePrefabName, position);
		stone.GetComponent<StoneView> ().StoneColor = color;
		
		return stone;
	}

	public static GameObject CreateGroup(Constants.StoneColor color, Vector2[] positions){
		var go = new GameObject ();
		MeshFilter mf = (MeshFilter)go.AddComponent("MeshFilter");
		MeshRenderer mr = (MeshRenderer)go.AddComponent("MeshRenderer");
		Mesh mesh = createMesh();
		
		mr.material = _material;
		mr.material.color = Color.grey;
		
		mf.mesh = mesh;

		go.AddComponent("StoneView");
		stone.GetComponent<StoneView> ().StoneColor = color;
	}
	
	private static Mesh createMesh ()
	{
		var result = new Mesh ();
		var vertices = new Vector3[182];
		float radius = 0.5f;
		Vector2 center = Vector2.zero;
		
		vertices [0] = new Vector3 (center.x, center.y, -.5f);
		for (int i = 1; i < vertices.Length; i++) {
			float x = center.x + radius * Mathf.Cos(2*(i-1) * Mathf.PI / 180f);
			float y = center.y + radius * Mathf.Sin(2*(i-1) * Mathf.PI / 180f);
			vertices[i] = new Vector3(x,y,0);
		}
		
		var uvs = new Vector2[vertices.Length];
		
		for (int i = 0; i < uvs.Length; i++) {
			uvs[i] = (i%2 == 0) ? Vector2.zero : new Vector2(1,1);
		}
		
		var triangles = new int[3 * (vertices.Length - 2)];
		var C2 = vertices.Length - 1;
		var C3 = vertices.Length - 2;
		
		for (int i = 0; i < triangles.Length; i+=3) {
			triangles[i] = 0;
			triangles[i+1] = C2;
			triangles[i+2] = C3;
			
			C2--;
			C3--;
		}
		
		result.vertices = vertices;
		result.uv = uvs;
		result.triangles = triangles;
		
		result.RecalculateNormals ();
		result.RecalculateBounds ();
		result.Optimize ();
		
		return result;
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
