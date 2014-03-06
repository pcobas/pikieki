using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public Vector3 startAt,finishAt;
	public float speed=1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (speed*Time.deltaTime,0,0);

	}
}
