using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour
{
		float timesound = 0;
		public float timeBetweenSounds = 0.1f;
		public bool canPlaySound = true;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{		if(!canPlaySound){
			Debug.Log ("cant");
				timesound+=Time.deltaTime;
				if (timesound > timeBetweenSounds) {
						canPlaySound = true;
				}
		}
	
		}

		void OnCollisionEnter2D (Collision2D c)
		{
				if (canPlaySound) {
						if (c.relativeVelocity.sqrMagnitude > 10.65f) {
								canPlaySound = false;
								SoundManager.PlaySFX (SoundManager.Load ("hit"));
								timesound=0f;
								Debug.Log (c.relativeVelocity.sqrMagnitude);
						}
				}
		}


}
