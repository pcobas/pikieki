using UnityEngine;
using System.Collections;

public class DestroyBody : MonoBehaviour
{

		public float timer = 2;
		public float time_to_destroy = 2;
		public Transform lastTransform = null;
		// Use this for initialization
		void Start ()
		{
				timer = time_to_destroy;
		}
	
		// Update is called once per frame
		void Update ()
		{
				Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
				if (Input.GetMouseButton (0)) {
						RaycastHit2D hit = Physics2D.Raycast (new Vector2 (pos.x, pos.y), Vector2.up);
						if (hit != null) {
								if (hit.transform != null) {
										if (hit.transform.tag != "Player") {
												if (lastTransform != null) {
														if (lastTransform != hit.transform && timer > 0) {
																timer = time_to_destroy;
																lastTransform = null;
														}
														timer -= Time.deltaTime;
														if (timer < 0) {
																Destroy (hit.transform.gameObject);
																lastTransform = null;
																timer = time_to_destroy;
														}
												} else {
														lastTransform = hit.transform;
												}
												Debug.Log (hit.transform.name);
										}

								}
						}
		
				}
				if (Input.GetMouseButtonUp (0)) {
						lastTransform = null;
						timer = time_to_destroy;
				}
		}
}
