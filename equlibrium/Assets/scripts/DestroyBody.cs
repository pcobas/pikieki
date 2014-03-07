using UnityEngine;
using System.Collections;

public class DestroyBody : MonoBehaviour
{

		public float timer = 2;
		public float time_to_destroy = 3;
		public Transform lastTransform = null;
		public bool canDestroy = true;
	public UISprite sprite;
		// Use this for initialization
		void Start ()
		{
				timer = time_to_destroy;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!canDestroy) {
						timer += Time.deltaTime;
						Debug.Log ("counting ");
						if (timer >= this.time_to_destroy) {
								canDestroy = true;
						}
					float percent=timer/time_to_destroy;
					sprite.fillAmount=percent;
					if(sprite.fillAmount>=1)
				sprite.fillAmount=0;

				}
				Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
				if (Input.GetMouseButtonDown (0)) {
						RaycastHit2D hit = Physics2D.Raycast (new Vector2 (pos.x, pos.y), Vector2.up);
						if (hit != null) {
								if (hit.transform != null) {
										if (hit.transform.tag != "Player") {
												if (canDestroy) {
														canDestroy = false;
														Destroy (hit.transform.gameObject);
														timer = 0;
														GameControl.Instance.IncreaseMoves ();
												} 
										}
								}
						}
		
				}
				
		}
}
