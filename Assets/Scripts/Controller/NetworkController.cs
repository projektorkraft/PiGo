using UnityEngine;
using System.Collections;
using GeoLib;
using System;

public class NetworkController : MonoBehaviour
{
	private int _port = 25000;
	private bool _connected;

	[HideInInspector]
	public Action<C2DPoint> OnMessageReceived;

	public void BeServer(){
		if (_connected) return;
		// Creating server
		Network.InitializeServer(1, _port, false);
		_connected = true;
	}

	public void ConnectToServer(string ip){
		if (_connected) return;
		// Connecting to the server
		Network.Connect(ip, _port);
		_connected = true;
	}

	void OnConnectedToServer() {
		Debug.Log("Connected to server");
	}

	[RPC]
	void ReceivePoint(float x, float y){
		OnMessageReceived (new C2DPoint(x,y));
	}

	public void SendPoint(C2DPoint point){
		networkView.RPC("ReceivePoint", RPCMode.Others, (float)point.x, (float)point.y);
	}
}