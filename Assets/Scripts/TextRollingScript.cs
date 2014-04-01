using UnityEngine;
using System.Collections;

public class TextRollingScript : MonoBehaviour {
	string textbuffer = ""; // a buffer to store text that hasn't yet been rolled to the screen
	float timeElapsed = 0f;

	const float TEXT_ROLLING_SPEED = 0.015f;

	// some other script will input add messages into this string
	public string input_text = null;
	bool ready_to_receive_output = true;

	int num_chars = 0;
	const int NUMCHARS_PER_LINE = 25;
	string output = "";

	// Use this for initialization
	void Start () {
		input_text = "Welcome to the Shroom World!\n\nPlayer 1 is yellow. Use [ WASD ] to move and [ Space ] to shoot the ball. \n\nPlayer 2 is red. Use the arrow keys to move and Keypad 0 to shoot the ball. \n\nPlayer 2 starts with the ball.";
	}
	
	// Update is called once per frame
	void Update () {
		if (ready_to_receive_output && input_text != null) {
			textbuffer = input_text;
			//GetComponent<messageBox>().stringToEdit = "";
			input_text = null;
			timeElapsed = 0f;
			ready_to_receive_output = false;
			num_chars = 0;
		}

		if (!ready_to_receive_output) {
			if (textbuffer.Length == 0) {
				output = "";
				ready_to_receive_output = true;
			}
			else {
				timeElapsed += Time.deltaTime;
				if (timeElapsed > TEXT_ROLLING_SPEED) {
					timeElapsed = 0;
					
					output += textbuffer[0];
					GetComponent<messageBox>().stringToEdit = output;
					
					textbuffer = textbuffer.Substring(1);
				}
			}
		}
	}
}
