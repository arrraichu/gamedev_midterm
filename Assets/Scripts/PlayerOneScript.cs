using UnityEngine;
using System.Collections;

public class PlayerOneScript : MonoBehaviour {

	const float FORWARD_FORCE = 0.2f;
	const float JUMP_FORCE = 0.5f;
	const float SINGLE_JUMP_RANGE = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit rayHit = new RaycastHit();

		if (Input.GetKey(KeyCode.W)) {
			rigidbody.AddForce(transform.forward * FORWARD_FORCE, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.S)) {
			rigidbody.AddForce(-transform.forward * FORWARD_FORCE, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.D)) {
			rigidbody.AddForce(transform.right * FORWARD_FORCE, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.A)) {
			rigidbody.AddForce(-transform.right * FORWARD_FORCE, ForceMode.VelocityChange);
		}

		if (Input.GetKey(KeyCode.Space) && Physics.Raycast(ray, out rayHit, SINGLE_JUMP_RANGE)) {
			rigidbody.AddForce(transform.up * JUMP_FORCE, ForceMode.Impulse);
		}
	}
}
