using UnityEngine;
using System.Collections;

public class Balancer : MonoBehaviour {

	public bool enableHinge;
	public GameObject joint;
	// Use this for initialization
	void Start () {
	

			this.joint.SetActive (this.enableHinge);
		this.rigidbody2D.isKinematic=!this.enableHinge;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
