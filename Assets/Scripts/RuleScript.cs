using UnityEngine;
using System.Collections;

public class RuleScript : MonoBehaviour {

	public GameObject gameBall;
	public bool gamereset = false;

	public GameObject yellowBox;
	public GameObject greenBox;
	public GameObject blueBox;
	public GameObject redBox;

	public Material lightYellow;
	public Material lightGreen;
	public Material lightBlue;
	public Material lightRed;

	public Material originalYellow;
	public Material originalGreen;
	public Material originalBlue;
	public Material originalRed;

	// Use this for initialization
	void Start () {
		Debug.Log(GetComponent<Rulees>().rule);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		int rule = GetComponent<Rulees>().rule;

		if (!gameBall.GetComponent<BallScript>().gameMode) {
			string explain = "";
			switch (rule) {
				case 1:
					explain = "\n\nCALL NUMBERS\nScream your number to your players before you hit the ball!";
					break;
				case 2:
					explain = "\n\nTEAM UP\nChoose another color to team up with you! Press [6][7][8][9] to tag with the yellow, green, blue, or red mushroom.";
					break;
				case 3:
					explain = "\n\nGRAVITY\nGravity will change randomly!";
					break;
				default:
					explain = "";
					break;
			}

			GetComponent<Rulees>().extra = explain;
			gamereset = false;
		}
		else if (!gamereset) {
			reset();
			if (rule == 0) return;

			else if (rule == 3) {
				float gravity = -9.81f + Random.Range(-2f, 8f);
				Physics.gravity = new Vector3(0, gravity, 0);
			}

			else if (rule == 2) {
				string explain = "";
				string sourcecolor = GetComponent<Rulees>().tagcolor;
				string tagcolor = gameBall.GetComponent<BallScript>().hitSource;

				GameObject target = null;
				if (sourcecolor == "yellow") {
					target = yellowBox;
				}
				else if (sourcecolor == "green") {
					target = greenBox;
				}
				else if (sourcecolor == "red") {
					target = redBox;
				}
				else if (sourcecolor == "blue") {
					target = blueBox;
				}

				Material changer = null;
				if (tagcolor == "yellow") {
					changer = lightYellow;
				}
				else if (tagcolor == "green") {
					changer = lightGreen;
				}
				else if (tagcolor == "red") {
					changer = lightRed;
				}
				else if (tagcolor == "blue") {
					changer = lightBlue;
				}

				target.renderer.material = changer;

				explain += "\n\nTeaming up with mushroom " + sourcecolor;

				GetComponent<Rulees>().extra += explain;

			}

			gamereset = true;
		}
	}

	void reset() {
		GetComponent<Rulees>().rule = 0;

		Physics.gravity = new Vector3(0, -9.81f, 0);
		yellowBox.renderer.material = originalYellow;
		greenBox.renderer.material = originalGreen;
		blueBox.renderer.material = originalBlue;
		redBox.renderer.material = originalRed;
	}
}
