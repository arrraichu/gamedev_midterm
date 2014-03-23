using UnityEngine;
using System.Collections;

public class points : MonoBehaviour {

	int score = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		GetComponent<TextMesh> ().text = "P1\nScore: " + score;
		
		
		
	}
	
	void FixedUpdate() {
		if (Input.GetKey (KeyCode.V))
			IncrementScore ();
	}
	
	void IncrementScore() {
		++score;
	}

}
