using UnityEngine;
using System.Collections;

public class sin : MonoBehaviour {
	public float amount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float y=Mathf.Sin (Time.time*amount);
		this.transform.position=new Vector3(this.transform.position.x,this.transform.position.y+y*.5f,this.transform.position.z);
	}
}
