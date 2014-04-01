using UnityEngine;
using System.Collections;

public class ComputerScript : MonoBehaviour {

	public GameObject gameball;
	public GUIText texture;
	public string inColor;
	int computer = 0;

	const float VISION_THRESHOLD = 10f;
	const float ROTATION_FORCE = 2.5f;
	const float MOVEMENT_SPEED = 0.1f;
	const float GO_BACK_SPEED = 80f;
	const float DISTANCE_THRESH = 1.6f;

	const float STARTING_FORWARD_FORCE = 6f;
	const float STARTING_FORWARD_AMOUNT = 3.2f;
	const float UP_FORCE = 0.2f;

	bool increased_score = false;

	// starting x and z positions of the computer
	float STARTING_X;
	float STARTING_Z;

	float time_elapsed = 0f;
	const float WAITING_TIME = 5f;

	// Use this for initialization
	void Start () {
		switch (inColor) {
			case "green":
				computer = 3; break;
			case "yellow":
				computer = 4; break;
			case "blue":
				computer = 1; break;
			case "red":
				computer = 2; break;
			default:
				computer = 0; break;
		}

		STARTING_X = (computer >= 3) ? -5f : 5f;
		STARTING_Z = (computer == 1 || computer == 4) ? -5f : 5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameball.GetComponent<BallScript>().gameMode) {
			if (gameball.GetComponent<BallScript>().hitSource == inColor) {
				if (increased_score == false) {
					increased_score = true;
					int score = int.Parse(texture.text) + 1;
					texture.text = "" + score;
				}
				startTimer();
				returnToLocation();
				return;
			}
			else {
				returnToLocation();
				return;
			}
		}


		if (expectedLanding()) {
			if (!inMyBox(transform.position.x, transform.position.z)) {
				returnToLocation();
			}
			chaseBall();
		}
		else {
			returnToLocation();
		}
	}

	// Returns true if computer thinks its going to land on his spot
	bool expectedLanding() {
		float time_left;

		// if the ball the still going upwards, then use gravity to calculate time
		if (gameball.rigidbody.velocity.y >= 0) {
			time_left = Mathf.Sqrt(gameball.transform.position.y / -Physics.gravity.y);
		}
		// otherwise, use velocity to calculate time
		else {
			// time = position / velocity
			time_left = gameball.transform.position.y / -gameball.rigidbody.velocity.y; 
		}

		float x = gameball.transform.position.x + (time_left * gameball.rigidbody.velocity.x); // x(t) = x(0) + t*v(t)
		float z = gameball.transform.position.z + (time_left * gameball.rigidbody.velocity.z); // z(t) = z(0) + t*v(t)

		// since the center of the playing field is (0,0), 
		// whether x and z are +/- will determine where the ball is expected to land
		if (inMyBox(gameball.transform.position.x, gameball.transform.position.z) || inMyBox(x, z)) return true;
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
		else if (computer == 4) {
			if (x <= 0 && z <= 0) return true;
			return false;
		}
		return false;
	}

	// Move the computer (DO CHECKS OUTSIDE THIS FUNCTION)
	void chaseBall() {
		// check gameball distances only by xz values
		Vector3 ball_check_position = gameball.transform.position;
		ball_check_position.y = 0;

		// if position is less than the distance threshold then do nothing
		if (Vector3.Distance(ball_check_position, transform.position) < DISTANCE_THRESH) return;

		// get the forward, backward, left, and right vectors
		Vector3 forward = ball_check_position - (transform.position + (transform.forward * MOVEMENT_SPEED));
		Vector3 backward = ball_check_position - (transform.position + (-transform.forward * MOVEMENT_SPEED));
		Vector3 right = ball_check_position - (transform.position + (transform.right * MOVEMENT_SPEED));
		Vector3 left = ball_check_position - (transform.position + (-transform.right * MOVEMENT_SPEED));

		// get the smallest distance vector
		Vector3 min_vector;
		Vector3 min1 = (Mathf.Min(forward.magnitude, backward.magnitude) == forward.magnitude) ? forward : backward;
		Vector3 min2 = (Mathf.Min(right.magnitude, left.magnitude) == right.magnitude) ? right : left;
		min_vector = (Mathf.Min(min1.magnitude, min2.magnitude) == min1.magnitude) ? min1 : min2;

		Vector3 move_to = -min_vector - transform.position + ball_check_position;
		move_to.y = 0;
		transform.position += move_to;
	}

	void returnToLocation() {		
		Vector3 goback = new Vector3(STARTING_X, transform.position.y, STARTING_Z) - transform.position;
		goback.Normalize();

		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce(goback * GO_BACK_SPEED, ForceMode.Force);
	}

	void startTimer() {
		time_elapsed += Time.deltaTime;

		if (time_elapsed > WAITING_TIME) {
			time_elapsed = 0f;

			// determine rotation by the computer panel color
			float rotation_angle = 0;
			if (inColor ==  "blue") {
				rotation_angle = 270f;
			}
			else if (inColor == "green") {
				rotation_angle = 90f;
			}
			rotation_angle += Random.Range(0f, 90f);
			rotation_angle *= Mathf.PI / 180;

			Vector3 forward_force = new Vector3(Mathf.Sin(rotation_angle), UP_FORCE, Mathf.Cos(rotation_angle));

			gameball.transform.position = transform.position + (forward_force * STARTING_FORWARD_AMOUNT);

			gameball.rigidbody.active = true;
			gameball.renderer.enabled = true;
			gameball.GetComponent<BallScript>().gameMode = true;
			gameball.GetComponent<BallScript>().bounce = 0;

			gameball.rigidbody.AddForce(forward_force * STARTING_FORWARD_FORCE, ForceMode.VelocityChange);
		}
	}
}
