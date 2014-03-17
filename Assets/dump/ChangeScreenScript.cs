using UnityEngine;
using System.Collections;

public class ChangeScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return)) {
			Application.LoadLevel("midterm");
		}
		if (Input.GetKey(KeyCode.Z)) {
			Application.LoadLevel("mainscreen");
			
		}
	}
}
