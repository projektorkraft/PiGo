using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
	private int _port = 25000;
	private bool _connected;

	public void BeServer(){
		if (_connected) return;
		// Creating server
		Network.InitializeServer(1, _port, false);
		_connected = true;

		/* Needed?
		// Notify our objects that the level and the network is ready
		foreach (var go in FindObjectsOfType<GameObject>())
		{
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver); 
		}
		*/

	}

	public void ConnectToServer(string ip){
		if (_connected) return;
		// Connecting to the server
		Network.Connect(ip, _port);
		_connected = true;
	}
}