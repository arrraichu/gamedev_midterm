using UnityEngine;
using System.Collections;

public class randomcoloring : MonoBehaviour {

	//public Color colorStart = Color.red;
	//public Color colorEnd = Color.green;
//	public float duration = 1.0F;
	public float timer = 0f;
//	public float durationInv;
	// Use this for initialization
	public float previous = 1f;
	public bool disappearing = true;
	double previousTime = 0;
	int previousTimeInt = 0;
	float r;
	float b;
	float g;
	void Start () {
	}


	
	// Update is called once per frame
	void Update () {
//		if (Time.time % 10 >= 7) {
//			return;
//				}

		float time = Time.time % 7;
		if (time != 0) {
			time = time / (float)7.0;
				}
		float alpha = Mathf.Lerp( 1f, 0f, time );

//		float alpha = Mathf.Lerp( 1f, 0f, timer  );
		if (alpha > previous) {
			disappearing = !disappearing;

		}
		previous = alpha;
		if (!disappearing) {
			alpha = 1 - alpha;
		}
		var epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
		var timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
		var timeStampInt = (int)timestamp;



		int timeDiff = 0;
		if( previousTime != 0 ){
			timeDiff = ((int) ((timestamp - previousTime) * 1000) % 10 ); 
		}
		Debug.Log ("abc:" + timeDiff);
		if ( previousTimeInt == 0 || (timeStampInt - previousTimeInt == 1) ) { //1 second
//		if ( previousTime == 0 || (timeDiff == 5) ) {//less than 1 second
			r = Random.Range (0, 100);
			g = Random.Range (0, 100);
			b = Random.Range (0, 100);
		}
		previousTime = timestamp;
		previousTimeInt = timeStampInt;
		GetComponent<TextMesh>().color = new Color( r == 0 ? r : r/ 100, g == 0 ? g : g/100, b == 0 ? b : b / 100, alpha );


	}


}
