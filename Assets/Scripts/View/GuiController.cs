using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour {
	
	public static int menuWidth = 450;
		
	private bool gameRunning = false;
	private static int MAIN = 0;
	private static int CREDITS = 1;
	private int menuState = MAIN;
		
		//TODO: UserInterface
		public Rect _menuPosition = new Rect (Screen.width / 2f - menuWidth /2f, 200, menuWidth, Screen.height - 400);
		public Rect _copyRightPosition = new Rect (Screen.width - 150, Screen.height - 30, 150, 30);
		
		
		public void startGame() {
			gameRunning = true;
		}
		
		public bool isGameRunning() {
			return gameRunning;
		}
		
		// Use this for initialization
		void Start () 
		{	
			//TODO: UserInterface
			_menuPosition = new Rect (Screen.width / 2f - menuWidth /2f, 200, menuWidth, Screen.height - 400);
			_copyRightPosition = new Rect (Screen.width - 150, Screen.height - 30, 150, 30);					
		}
		
		void OnGUI(){
			if (menuState == MAIN) {
				GUILayout.BeginArea(_menuPosition);
				GUILayout.Box("Lobby Screen");
				
				if (GUILayout.Button("Credits")) {
					menuState = CREDITS;
				}
				if (GUILayout.Button("Start")) {
					startGame ();
				}
				GUILayout.EndArea();
			} else if (menuState == CREDITS) {
				GUILayout.BeginArea(_menuPosition);
				GUILayout.Box("Credits");
				GUILayout.TextArea("this awesome game was brought to you by : \n Anja Leßmann \n Sören Titze \n Michael Budahn");
				if (GUILayout.Button("back")) {
					menuState = MAIN;
				}
				GUILayout.EndArea();
			}
			
			GUILayout.BeginArea(_copyRightPosition);
			GUILayout.Label("(C) projektorkraft 2014");
			GUILayout.EndArea();
		}
		
	}