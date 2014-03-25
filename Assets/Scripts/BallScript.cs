using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	const float UP_FORCE = 0.6f;
	const float BOUNCE_FORCE = 1.8f;
	const float BOUNCE_LISTENING_RESET_HEIGHT = 1.4f;
	bool INCREMENT_OKAY = true;

	public int bounce = 0;
	public bool gameMode = false;
	public string hitSource = "yellow";


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameMode == true) game();		 
	}

	void game() {
		if (bounce >= 2) {
			gameMode = false;
			return;
		}

		if (INCREMENT_OKAY && transform.position.y < BOUNCE_LISTENING_RESET_HEIGHT && rigidbody.velocity.y < 0) {
			INCREMENT_OKAY = false;
			++bounce;
		}

		if (!INCREMENT_OKAY && transform.position.y > BOUNCE_LISTENING_RESET_HEIGHT) {
			INCREMENT_OKAY = true;
		}
	}

	// Called whenever a collision happens. collision will contain an array of things that the ball collider
	void OnCollisionEnter(Collision collision) {

		rigidbody.AddForce(Vector3.up * UP_FORCE, ForceMode.VelocityChange);	
		
		foreach (ContactPoint contacts in collision.contacts) {
			if (contacts.otherCollider.GetType().ToString() == "UnityEngine.CapsuleCollider") {
				hitSource = source(contacts.otherCollider.transform.position.x, contacts.otherCollider.transform.position.z);
				bounce = 0;
				return;
			}
		}	
	}

	string source(float x, float z) {
		if (x > 0 && z > 0) return "red";
		if (x <= 0 && z <= 0) return "yellow";
		if (x > 0 && z <= 0) return "blue";
		if (x <= 0 && z > 0) return "green";
		return "none";
	}
}
