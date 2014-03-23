using UnityEngine;
using System.Collections;

public class TextRollingScript : MonoBehaviour {
	string textbuffer = ""; // a buffer to store text that hasn't yet been rolled to the screen
	float timeElapsed = 0f;

	const float TEXT_ROLLING_SPEED = 0.025f;

	// some other script will input add messages into this string
	public string input_text = null;
	bool ready_to_receive_output = true;
	
	Rulees rules = GameObject.GetComponent <Rulees>();

	int num_chars = 0;
	const int NUMCHARS_PER_LINE = 25;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ready_to_receive_output && input_text != null) {
			textbuffer = input_text;
			GetComponent<rules>().text = "";
			input_text = null;
			timeElapsed = 0f;
			ready_to_receive_output = false;
			num_chars = 0;
		}

		if (!ready_to_receive_output) {
			if (textbuffer.Length == 0) {
				ready_to_receive_output = true;
			}
			else {
				timeElapsed += Time.deltaTime;
				if (timeElapsed > TEXT_ROLLING_SPEED) {
					timeElapsed = 0;
					if (++num_chars >= NUMCHARS_PER_LINE && textbuffer[0] == ' ') {
						GetComponent<rules>().text += "\n";
						num_chars = 0;
					}
					else {
						GetComponent<rules>().text += textbuffer[0];
					}
					textbuffer = textbuffer.Substring(1);
				}
			}
		}
	}
}
