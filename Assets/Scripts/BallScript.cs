using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	const float UP_FORCE = 0.8f;
	const float BOUNCE_FORCE = 1.8f;
	const float BOUNCE_LISTENING_RESET_HEIGHT = 1.4f;

	public int bounce = 0;
	public bool gameMode = false;
	public string hitSource;

	const float HALF_WIDTH = 10f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameMode == true) game();

		else {
			rigidbody.active = false;
			renderer.enabled = false;
		}	 
	}

	void game() {
		if (bounce >= 2) {
			gameMode = false;
			bounce = 0;
			return;
		}
	}

	// Called whenever a collision happens. collision will contain an array of things that the ball collider
	void OnCollisionEnter(Collision collision) {

		rigidbody.AddForce(Vector3.up * UP_FORCE, ForceMode.VelocityChange);	
		
		foreach (ContactPoint contacts in collision.contacts) {
			if (gameMode && contacts.otherCollider.name == "Floor") {
				++bounce;
				return;
			}

			string contact_name = contacts.otherCollider.name;
			if (contact_name == "Player 1" || contact_name == "Player 2" || contact_name == "Computer 1" || contact_name == "Computer 2") {
				hitSource = source(contact_name);
				bounce = 0;
				return;
			}

			Debug.Log(contacts.otherCollider.name);
		}	
	}

	string source(string input) {
		if (input == "Player 1") return "yellow";
		if (input == "Player 2") return "red";
		if (input == "Computer 1") return "green";
		if (input == "Computer 2") return "blue";
		return "none";
	}
}
