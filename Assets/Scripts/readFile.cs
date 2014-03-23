using UnityEngine;
using System.Collections;
using System.IO;

public class readFile : MonoBehaviour {


	// Use this for initialization
	void Start () {
		try {
			StreamReader sr = new StreamReader ("readMe.txt");
			while (!sr.EndOfStream){
				string line = sr.ReadLine();
				Debug.Log(line);
			}
		}
		catch (IOException e){
			Debug.Log(e);
		}

	}
	
	// Update is called once per frame
	void Update () {

		
	}
	

}
