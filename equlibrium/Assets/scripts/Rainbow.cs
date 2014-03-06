using UnityEngine;
using System.Collections;

public class Rainbow : MonoBehaviour {
	public float targetAngle=0;
	public float time =1f;
	public float speed;
	public GameObject Text_Win;
	// Use this for initialization

	void OnDisable(){
		this.transform.rotation=Quaternion.Euler (0,0,95);
	}
	void OnEnable(){
		//iTween.RotateTo (this.gameObject,Vector3.zero,time);
	}

	public void Update(){
		if(this.transform.rotation.eulerAngles.z>0){
		this.transform.rotation=Quaternion.Euler (0,0,this.transform.rotation.eulerAngles.z-this.speed*Time.deltaTime);
		}
		if(this.transform.rotation.eulerAngles.z<0||this.transform.rotation.eulerAngles.z>100){
			this.transform.rotation=Quaternion.identity;
			Text_Win.SetActive (true);
		}
	}
}
