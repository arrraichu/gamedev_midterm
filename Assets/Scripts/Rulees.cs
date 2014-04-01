using UnityEngine;
using System.Collections;

public class Rulees : MonoBehaviour {
	string stringToEdit;
	string buffer;
	float time_elapsed = 0f;
	const float TOTAL_STAY_TIME = 5f;

	public int rule = 0;
	public string tagcolor;
	string append = null;
	string intro = "Choose your rule \n My King  \n\n1. Call the number \n2. Team Up \n3. Gravity Challenge\n\nPress the rule's number to turn on the rule. Press 0 to reset.";
	public string extra;

	// Use this for initialization
	void Start () {
		
	}
	void OnGUI() {

		stringToEdit = GUI.TextArea(new Rect(758, 0, 200, 300), stringToEdit, 500);
	}
	
	void FixedUpdate() {
		if (Input.GetKey(KeyCode.Alpha0)) {
			rule = 0;
		}
		else if (Input.GetKey(KeyCode.Alpha1)) {
			rule = 1;
		}
		else if (Input.GetKey(KeyCode.Alpha2)) {
			rule = 2;
		}
		else if (Input.GetKey(KeyCode.Alpha3)) {
			rule = 3;
		}

		if (rule == 0) {
			append = "\n\nYou currently have no rules set.";
		}
		else {
			append = "\n\nRule # " + rule + " is turned on.";
		}

		stringToEdit = intro + append + extra;

		if (rule == 2) {
			if (Input.GetKey(KeyCode.Alpha6)) {
				tagcolor = "yellow";
			}
			else if (Input.GetKey(KeyCode.Alpha7)) {
				tagcolor = "green";
			}
			else if (Input.GetKey(KeyCode.Alpha8)) {
				tagcolor = "blue";
			}
			else if (Input.GetKey(KeyCode.Alpha9)) {
				tagcolor = "red";
			}
		}
	}
}
