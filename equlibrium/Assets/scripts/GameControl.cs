using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SmoothMoves;
public class GameControl : MonoBehaviour {

	public static GameControl Instance;
	public List<PieceLocation> pieces;
	public GameObject Rainbow;
	public TextMesh moves_textmesh;
	public int moves;
	public BoneAnimation ground;
	public bool player_won;
	public static string level_to_play; 
	public static bool is_custom_level;
	// Use this for initialization
	void Start () {
		Debug.Log (Application.persistentDataPath);
		Instance=this;
		if(is_custom_level){
		LoadLevel (level_to_play);
		ground.Play ("lookUp");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void GoToMenu(){
		Application.LoadLevel("menu");
	}
	public void Win(){
		if(!player_won){
		Rainbow.SetActive (true);
		Debug.Log ("WON");
		ground.Play ("smile");
			player_won=true;
		}
	}

	public void IncreaseMoves(){
		moves++;
		moves_textmesh.text=moves.ToString ();
	}

	public void LoadLevel(string file){
		Rainbow.SetActive (false);
		string json = ES2.Load<string> (file + ".json");
		Debug.Log (json);
		foreach (PieceLocation p in pieces) {
			Destroy (p.piece);
		}
		pieces = DeserializePieces (json);
	}
	public List<PieceLocation> DeserializePieces (string json)
	{
		List<PieceLocation> pieces_list = new List<PieceLocation> ();
		SerializablePieceLocation[] sp_array = LitJson.JsonMapper.ToObject<SerializablePieceLocation[]> (json);
		foreach (SerializablePieceLocation spl in sp_array) {
			PieceLocation p = new PieceLocation ();
			p.parseSerializable (spl);
			p.piece = (GameObject)Instantiate (Resources.Load (spl.piece_name), p.position, p.rotation);
			pieces_list.Add (p);
		}
		
		return pieces_list;
		
	}
}
