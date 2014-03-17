using UnityEngine;
using System.Collections;

public class HighScoreScript : MonoBehaviour {

	static int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.P)) ++score;
		if (Input.GetKey(KeyCode.O)) Application.LoadLevel(1);
	}
}
