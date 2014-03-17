using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	const float MOVEMENT_SPEED = 0.15f;
	const float SINGLEJUMP_RANGE = 0.7f;
	const float JUMPING_FORCE = 180f;
	const float ROTATION_FORCE = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Z)) audio.Play();
	}

	void FixedUpdate() {
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit rayhit = new RaycastHit();

		// D-PAD
		if (Input.GetKey(KeyCode.W)) {
			rigidbody.AddForce(transform.forward * MOVEMENT_SPEED, ForceMode.VelocityChange);
		}
		else if (Input.GetKey(KeyCode.A)) {
			rigidbody.AddForce(-transform.right * MOVEMENT_SPEED, ForceMode.VelocityChange);
		}
		else if (Input.GetKey(KeyCode.D)) {
			rigidbody.AddForce(transform.right * MOVEMENT_SPEED, ForceMode.VelocityChange);
		}
		else if (Input.GetKey(KeyCode.S)) {
			rigidbody.AddForce(-transform.forward * MOVEMENT_SPEED, ForceMode.VelocityChange);
		}

		// ROTATIONS
		if (Input.GetKey(KeyCode.Q)) {
			transform.Rotate(-transform.up * ROTATION_FORCE);
		}
		else if (Input.GetKey(KeyCode.E)) {
			transform.Rotate(transform.up * ROTATION_FORCE);
		}

		// JUMPING
		if (Input.GetKey(KeyCode.Space) && Physics.Raycast(ray, out rayhit, SINGLEJUMP_RANGE)) {
			rigidbody.AddForce(transform.up * JUMPING_FORCE);

		}

		// PREVENTING MOVEMENT WHEN HITTING
		if (Input.GetMouseButtonDown(0)) {
			rigidbody.velocity = Vector3.zero;
		}
	}
}
