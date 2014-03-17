using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {

	public Transform player_pos;
	const float HORIZONTAL_MOUSE_ADJUST = 1.8f;
	const float VERTICAL_MOUSE_ADJUST = 1.3f;
	const float CAMERA_UP_ADJUST = 0.28f;
	const int HOR_OFFSCREEN_THRESHOLD = 50;
	const int VER_OFFSCREEN_THRESHOLD = 50;
	const float HITTING_FORCE = 1000f;
	const float HITTING_DURATION = 0.02f;

	float hitting_time = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (hitting_time > 0f) {
			hitting_time -= Time.deltaTime;
		}
		else if (hitting_time < 0f) {
			hitting_time = 0f;
		}
		else if (hitting_time == 0f) {
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
	}

	void FixedUpdate() {
		if (hitting_time == 0f && !checkThreshold(Input.mousePosition)) {
			transform.position = convertMouseToWorld(player_pos);
		}
		transform.eulerAngles = new Vector3(90, 0, -player_pos.eulerAngles.y);

		handHit();
	}

	Vector3 convertMouseToWorld(Transform player) {

		Vector3 mouse = Input.mousePosition;
		Vector3 output;

		mouse.x /= Screen.width; mouse.x -= 0.5f;
		mouse.y /= Screen.height; mouse.y -= 0.5f;

		output = player.position + player.forward;
		output += player.right * mouse.x * HORIZONTAL_MOUSE_ADJUST;
		output += player.up * mouse.y * VERTICAL_MOUSE_ADJUST;
		output += player.up * CAMERA_UP_ADJUST;
		
		return output;
	}

	void handHit() {
		Vector3 mouse = Input.mousePosition;

		if (Input.GetMouseButtonDown(0) && checkThreshold(mouse) == false) {
			transform.position = convertMouseToWorld(player_pos);
			rigidbody.AddForce(player_pos.forward * HITTING_FORCE);
			hitting_time = HITTING_DURATION;

		}
		else {
			
		}
	}

	bool checkThreshold(Vector3 mouse) {
		if (mouse.x < -HOR_OFFSCREEN_THRESHOLD || mouse.x > Screen.width + HOR_OFFSCREEN_THRESHOLD || mouse.y < -VER_OFFSCREEN_THRESHOLD || mouse.y > Screen.height + VER_OFFSCREEN_THRESHOLD) return true;
		return false;
	}
}
