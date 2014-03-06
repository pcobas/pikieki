using UnityEngine;
using System.Collections;

public class Berty : MonoBehaviour {


	public float timeToWin=2f;
	public float time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D c){
		if(c.gameObject.tag=="Player"){

		}
	}

	void OnCollisionExit2D(Collision2D c){
		time=0;
	}

	void OnCollisionStay2D(Collision2D c){
		if(c.gameObject.tag=="Player"){
			time+=Time.deltaTime;
			if(time>timeToWin){
				Debug.Log ("YEAH");
				GameControl.Instance.Win ();
			}
		}
	}
}
