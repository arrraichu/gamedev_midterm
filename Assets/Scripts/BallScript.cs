using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	const float UP_FORCE = 1f;
	const float BOUNCE_FORCE = 1.8f;

	public GameObject wall_x1, wall_x2, wall_z1, wall_z2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision) {
		rigidbody.AddForce(Vector3.up * UP_FORCE, ForceMode.VelocityChange);
		if (Input.GetKey(KeyCode.P)) rigidbody.AddForce(Vector3.up * 3f * UP_FORCE, ForceMode.VelocityChange);

		if (collision.contacts[0].thisCollider == wall_x1.collider) {
			rigidbody.AddForce(Vector3.forward * BOUNCE_FORCE, ForceMode.VelocityChange);
		}
		else if (collision.contacts[0].thisCollider == wall_x2.collider) {
			rigidbody.AddForce(-Vector3.forward * BOUNCE_FORCE, ForceMode.VelocityChange);
		}
		else if (collision.contacts[0].thisCollider == wall_z1.collider) {
			rigidbody.AddForce(Vector3.right * BOUNCE_FORCE, ForceMode.VelocityChange);
		}
		else if (collision.contacts[0].thisCollider == wall_z2.collider) {
			rigidbody.AddForce(-Vector3.right * BOUNCE_FORCE, ForceMode.VelocityChange);
		}
		
	}
}
