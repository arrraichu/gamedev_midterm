using UnityEngine;
using System.Collections;

public class Name : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent <TextMesh> ().text = "By: Man Chu, Shiny Croospulle \n      Qiao Jing Wu, Danny Katz";
	}
}
