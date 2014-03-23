using UnityEngine;
using System.Collections;

public class messageBox : MonoBehaviour {
	public string stringToEdit = "Message displays here :)";

	// Use this for initialization
	void Start () {
	
	}
	void OnGUI() {
		stringToEdit = GUI.TextArea(new Rect(758, 300, 200, 300), stringToEdit, 200);
	}

	// Update is called once per frame
	void Update () {
		if( Input.GetKey (KeyCode.Alpha1)) {
			stringToEdit = "You have to be in \nchange of two players, \nswitch to a different \nplayer for the next hitting. \nUse key Shift to switch.";
		}
		if( Input.GetKey (KeyCode.Alpha2)) {
			stringToEdit = "Call the number of the \nplayer you going to hit the \nball to before hitting the \nball.";
		}
		if( Input.GetKey (KeyCode.Alpha3)) {
			stringToEdit = "You team up with another \nplayer, and hit the ball to \nthe opponents' team.";
		}
		if( Input.GetKey (KeyCode.Alpha4)) {
			stringToEdit = "Gravity changed, hit the \nball in a different way.";
		}
		if (Input.GetKey (KeyCode.Alpha0)) {
			stringToEdit = "Message Displays here :)";
		}
	
	}
}
