using UnityEngine;
using System.Collections;

public class Rulees : MonoBehaviour {
	public string stringToEdit = "Choose your rule \n My King  \n\n1. Tag team \n2. Call the number \n3. Team Up \n4. Gravity Challenge";
	// Use this for initialization
	void Start () {
	
	}
	void OnGUI() {
		stringToEdit = GUI.TextArea(new Rect(758, 0, 200, 300), stringToEdit, 200);
	}
	// Update is called once per frame
	void Update () {

	}
}
