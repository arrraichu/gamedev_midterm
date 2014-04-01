using UnityEngine;
using System.Collections;

public class messageBox : MonoBehaviour {
	public string stringToEdit = null;

	public string buffer = null;
	public float time_elapsed = 0f;
	const float TOTAL_STAY_TIME = 5f;

	// Use this for initialization
	void Start () {
	
	}
	void OnGUI() {
		if (buffer != null) {
			GUI.TextArea(new Rect(758, 300, 200, 300), buffer, 500);
		}
		else  {
			GUI.TextArea(new Rect(758, 300, 200, 300), "", 500);
		}		
	}

	// Update is called once per frame
	void Update () {
		time_elapsed += Time.deltaTime;

		if (stringToEdit != null) {
			buffer = stringToEdit;
			time_elapsed = 0f;
		}
		else if (time_elapsed > TOTAL_STAY_TIME) {
			stringToEdit = null;
			buffer = null;
			time_elapsed = 0f;
		}
	
	}
}
