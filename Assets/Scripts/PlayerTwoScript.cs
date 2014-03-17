﻿using UnityEngine;
using System.Collections;

public class PlayerTwoScript : MonoBehaviour {

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

		if (Input.GetKey(KeyCode.UpArrow)) {
			rigidbody.AddForce(transform.forward * FORWARD_FORCE, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			rigidbody.AddForce(-transform.forward * FORWARD_FORCE, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			rigidbody.AddForce(transform.right * FORWARD_FORCE, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			rigidbody.AddForce(-transform.right * FORWARD_FORCE, ForceMode.VelocityChange);
		}

		if (Input.GetKey(KeyCode.Keypad0) && Physics.Raycast(ray, out rayHit, SINGLE_JUMP_RANGE)) {
			rigidbody.AddForce(transform.up * JUMP_FORCE, ForceMode.Impulse);
		}
	}
}
