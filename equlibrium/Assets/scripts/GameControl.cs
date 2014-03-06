using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public static GameControl Instance;

	public GameObject Rainbow;
	// Use this for initialization
	void Start () {
		Instance=this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Win(){
		Rainbow.SetActive (true);

	}

	public void LoadLevel(){
		Rainbow.SetActive (false);
	}
}
