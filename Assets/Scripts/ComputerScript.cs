using UnityEngine;
using System.Collections;

public class ComputerScript : MonoBehaviour {

	public GameObject gameball;
	//public GameObject paddle;
	public int computer;

	const float VISION_THRESHOLD = 10f;
	const float ROTATION_FORCE = 2.5f;
	const float MOVEMENT_SPEED = 0.1f;
	const float GO_BACK_SPEED = 1.4f;

	// starting x and z positions of the computer
	float STARTING_X;
	float STARTING_Z;

	bool returnyet = false;

	// Use this for initialization
	void Start () {
		STARTING_X = (computer == 3) ? -2.5f : 2.5f;
		STARTING_Z = (computer == 1) ? -2.5f : 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//paddle.transform.position = transform.position + (-transform.forward * 0.5f);

		//seeBall();
		if (expectedLanding()) {
			if (returnyet == false) {
				//Debug.Log("Computer " + computer + ": I need to hit the ball!");
				returnyet = true;
			}
			chaseBall();
		}
		else {
			returnToLocation();
			returnyet = false;
		}
	}

	// Handles when the computer must rotate left of right to look at the ball
	void seeBall() {
		Vector3 seeball = gameball.transform.position - transform.position;

		if (Vector3.Angle(seeball, transform.forward) > VISION_THRESHOLD) {
			// if the computer moved right and moved closer to the ball than he was before, then he should rotate to look right
			if (Vector3.Distance(transform.position + transform.right, gameball.transform.position) < Vector3.Distance(transform.position, gameball.transform.position)) {
				transform.Rotate(transform.up * ROTATION_FORCE);
			}
			// otherwise he should look left
			else {
				transform.Rotate(-transform.up * ROTATION_FORCE);
			}
		}
	}

	// Returns true if computer thinks its going to land on his spot
	bool expectedLanding() {
		float time_left = gameball.transform.position.y / -gameball.rigidbody.velocity.y; // time = position / velocity
		float x = gameball.transform.position.x + (time_left * gameball.rigidbody.velocity.x); // x(t) = x(0) + t*v(t)
		float z = gameball.transform.position.z + (time_left * gameball.rigidbody.velocity.z); // z(t) = z(0) + t*v(t)

		// since the center of the playing field is (0,0), 
		// whether x and z are +/- will determine where the ball is expected to land
		if (inMyBox(transform.position.x, transform.position.z) || inMyBox(x, z)) return true;
		return false;
	}

	// returns true if the xz values are in the comptuer's box
	bool inMyBox(float x, float z) {
		if (computer == 1) {
			if (x > 0 && z <= 0) return true;
			return false;
		}
		else if (computer == 2) {
			if (x > 0 && z > 0) return true;
			return false; 
		}
		else if (computer == 3) {
			if (x <= 0 && z > 0) return true;
			return false;
		}
		return false;
	}

	// Moves the computer so that the ball is directly in front of it
	void chaseBall() {
		Vector3 optimal_position = transform.position;

		// The computer must choose one of four movements: WASD
		// It will determine which with whichever is the shortest length
		float forward_distance = Vector3.Distance(gameball.transform.position, optimal_position + (transform.forward * MOVEMENT_SPEED));
		float backward_distance = Vector3.Distance(gameball.transform.position, optimal_position + (-transform.forward * MOVEMENT_SPEED));
		float left_distance = Vector3.Distance(gameball.transform.position, optimal_position + (-transform.right * MOVEMENT_SPEED));
		float right_distance = Vector3.Distance(gameball.transform.position, optimal_position + (transform.right * MOVEMENT_SPEED));

		float min = Mathf.Min(Mathf.Min(forward_distance, backward_distance), Mathf.Min(left_distance, right_distance));
		if (!inMyBox(gameball.transform.position.x, gameball.transform.position.z)) {
			if (computer == 2) Debug.Log("returning to location\n");
			returnToLocation();
		}
		else if (min == forward_distance) {
			//rigidbody.AddForce(transform.forward * MOVEMENT_SPEED, ForceMode.Force);
			transform.position += transform.forward * MOVEMENT_SPEED;
			if (computer == 2) Debug.Log("moving forward\n");	
		}
		else if (min == backward_distance) {
			//rigidbody.AddForce(-transform.forward * MOVEMENT_SPEED, ForceMode.Force);
			transform.position += -transform.forward * MOVEMENT_SPEED;
			if (computer == 2) Debug.Log("moving backward\n");	
			
		}
		else if (min == left_distance) {
			//rigidbody.AddForce(-transform.right * MOVEMENT_SPEED, ForceMode.Force);
			transform.position += -transform.right * MOVEMENT_SPEED;
			if (computer == 2) Debug.Log("moving left\n");	
		}
		else if (min == right_distance) {
			//rigidbody.AddForce(transform.right * MOVEMENT_SPEED, ForceMode.Force);
			transform.position += transform.right * MOVEMENT_SPEED;
			if (computer == 2) Debug.Log("moving right\n");	
		}
	}

	void returnToLocation() {		
		Vector3 goback = new Vector3(STARTING_X, transform.position.y, STARTING_Z) - transform.position;
		goback.Normalize();

		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce(goback * GO_BACK_SPEED, ForceMode.Force);
	}
}
