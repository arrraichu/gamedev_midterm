using UnityEngine;
using System.Collections;

public class PlayerTwoScript : MonoBehaviour {

	const float FORWARD_FORCE = 25f;
	const float JUMP_FORCE = 0.5f;
	const float SINGLE_JUMP_RANGE = 2f;

	public GameObject pointing_arrow;
	public GameObject gameball;
	public GUIText texture;
	const float PA_ROTATION_SPEED = 0.5f;

	const float STARTING_FORWARD_AMOUNT = 3.2f;
	const float STARTING_FORWARD_FORCE = 6f;
	const float STARTING_UP_AMOUNT = 0f;
	const float UP_FORCE = 0f;

	const string color = "red";
	bool serveMode = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (serveMode) {
			// reset velocity
			transform.position = new Vector3(5, transform.position.y, 5);
			rigidbody.velocity = Vector3.zero;

			SetArrowAngles();
			if (Input.GetKey(KeyCode.Keypad0)) {
				pointing_arrow.renderer.enabled = false;
				gameball.rigidbody.active = true;
				gameball.renderer.enabled = true;
				gameball.GetComponent<BallScript>().bounce = 0;

				float arrow_rotation = (pointing_arrow.transform.eulerAngles.y + 90) * Mathf.PI / 180;
				Vector3 forward_force = new Vector3(Mathf.Sin(arrow_rotation), UP_FORCE, Mathf.Cos(arrow_rotation));

				gameball.rigidbody.AddForce(forward_force * STARTING_FORWARD_FORCE, ForceMode.VelocityChange);

				gameball.GetComponent<BallScript>().gameMode = true;
				serveMode = false;
			}
		}

		else if (!gameball.GetComponent<BallScript>().gameMode) {
			// reset velocity
			transform.position = new Vector3(5, transform.position.y, 5);
			rigidbody.velocity = Vector3.zero;

			if (gameball.GetComponent<BallScript>().hitSource == color) {
				Arrow_RestartPosition();

				float arrow_rotation = (pointing_arrow.transform.eulerAngles.y + 90) * Mathf.PI / 180;
				Vector3 forward_force = new Vector3(Mathf.Sin(arrow_rotation), UP_FORCE, Mathf.Cos(arrow_rotation));

				gameball.transform.position = pointing_arrow.transform.position + (forward_force * STARTING_FORWARD_AMOUNT);
				pointing_arrow.renderer.enabled = true;
				serveMode = true;

				int score = int.Parse(texture.text) + 1;
				texture.text = "" + score;
			}
		}

		else {
			Ray ray = new Ray(transform.position, -transform.up);
			RaycastHit rayHit = new RaycastHit();

			if (Input.GetKey(KeyCode.UpArrow)) {
				rigidbody.AddForce(transform.forward * FORWARD_FORCE, ForceMode.Force);
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				rigidbody.AddForce(-transform.forward * FORWARD_FORCE, ForceMode.Force);
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.AddForce(transform.right * FORWARD_FORCE, ForceMode.Force);
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				rigidbody.AddForce(-transform.right * FORWARD_FORCE, ForceMode.Force);
			}

			if (Input.GetKey(KeyCode.Keypad0) && Physics.Raycast(ray, out rayHit, SINGLE_JUMP_RANGE)) {
				rigidbody.AddForce(transform.up * JUMP_FORCE, ForceMode.Impulse);
			}
		}
	}

	void Arrow_RestartPosition() {
		transform.position = new Vector3(5, transform.position.y, 5);
		pointing_arrow.transform.position = transform.position;
		pointing_arrow.transform.eulerAngles = new Vector3(0, 135, 0);
	}

	void SetArrowAngles() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			pointing_arrow.transform.Rotate(-pointing_arrow.transform.up * PA_ROTATION_SPEED);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			pointing_arrow.transform.Rotate(pointing_arrow.transform.up * PA_ROTATION_SPEED);
		}
	}
}
