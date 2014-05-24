using UnityEngine;
using System.Collections;
using System.Text;
using System.Net;
using System;

public class GuiController : MonoBehaviour {

	private bool _menuShown = true;
	private MenuState _menuState = MenuState.Main;
	private string _ips;
	private string _ip;

	private int _menuWidth = 450;
	private Rect _menuPosition;
	private Rect _copyRightPosition;

	public Action<string> ConnectServerAction;
	public Action BeServerAction;

	public void startGame(bool isServer) {
		_menuShown = false;


		if (!isServer) {
			ConnectServerAction(_ip);
		} else {
			BeServerAction();
		}
	}
		
	public bool IsMenu() {
		return _menuShown;
	}
		
	// Use this for initialization
	void Start () 
	{
		_menuPosition = new Rect (Screen.width / 2f - _menuWidth /2f, 200, _menuWidth, Screen.height - 400);
		_copyRightPosition = new Rect (Screen.width - 150, Screen.height - 30, 150, 30);

		var sb = new StringBuilder(Environment.NewLine);
		var ipEntry = Dns.GetHostEntry(Dns.GetHostName());
		
		foreach (var entry in ipEntry.AddressList){
			sb.Append(entry.ToString() + Environment.NewLine);
		}

		_ips = sb.ToString ();
		_ip = "Enter IP here.";
	}
		
	void OnGUI(){
		if (!_menuShown) return;

		if (_menuState == MenuState.Main) {
			GUILayout.BeginArea(_menuPosition);
			GUILayout.Box("πGo");

			if (GUILayout.Button("Show your IPs")) {
				_menuState = MenuState.ShowIPs;
			}
			if (GUILayout.Button("Be a server")) {
				startGame (true);
			}
			if (GUILayout.Button ("Connect to a server")){
				_menuState = MenuState.ShowConnect;
			}
			GUILayout.EndArea();
		} else if (_menuState == MenuState.ShowIPs) {
			GUILayout.BeginArea(_menuPosition);
			GUILayout.Box("Your IPs");				
			GUILayout.TextArea("A List of your IPs:"+_ips);
			if (GUILayout.Button("Back")) {
				_menuState = MenuState.Main;
			}
			GUILayout.EndArea();
		} else if (_menuState == MenuState.ShowConnect){
			GUILayout.BeginArea(_menuPosition);
			GUILayout.Box("Connect to a Server");
			_ip = GUILayout.TextField(_ip, 15);
			if (GUILayout.Button("Connect")) {
				startGame(false);
			}
			if (GUILayout.Button("Back")) {
				_menuState = MenuState.Main;
			}
			GUILayout.EndArea();
		}

		GUILayout.BeginArea(_copyRightPosition);
		GUILayout.Label("(C) projektorkraft 2014");
		GUILayout.EndArea();
	}

	enum MenuState{
		Main, ShowIPs, ShowConnect
	}
}