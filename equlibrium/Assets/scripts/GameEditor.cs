using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameEditor : MonoBehaviour
{

		
		public static GameEditor Instance;
		public GameObject selectedPiece;
		public int snapSize = 16;
		public bool isDraggingUI = false;
		public List<PieceLocation> pieces = new List<PieceLocation> ();
		public bool isPlaying = false;
		public GameObject saveInput;
		public UIInput saveName;
		public bool canMove = true;
		public GameObject squareback;
		public List<GameObject> gridsquares;
		public GameObject gridContainer;
		// Use this for initialization
		void Start ()
		{

				gridContainer = new GameObject ("GridContainer");
				Instance = this;
				//Load ("zxczczx");
				//CreateGridBackground ();
		}

		void CreateGridBackground ()
		{

				int left = -Screen.width / 2-snapSize/2;
				int bottom = -Screen.height / 2-snapSize/2;

				int cols = Screen.width / snapSize;
				int rows = Screen.height / snapSize;
		cols++;
		rows++;
				Color oddC = new Color (0.35f, 0.35f, 0.35f, 0.25f);
				Color evenC = new Color (0.65f, 0.65f, 0.65f, 0);

				for (int y=0; y<rows; y++) {
						for (int x=0; x<cols; x++) {
								GameObject go = (GameObject)Instantiate (squareback, new Vector3 ((x * snapSize) + left, (y * snapSize) + bottom, 1), Quaternion.identity);
								go.name = x + "x" + y;
								go.transform.localScale = new Vector3 (snapSize, snapSize);
								go.transform.parent = gridContainer.transform;
								if ((x * y) % 2 == 0)
										go.transform.renderer.material.color = evenC;
								else
										go.transform.renderer.material.color = oddC;
						}
				}

		}

		void OnEnable ()
		{
				//Camera.main.transform.GetChild (0).gameObject.SetActive (true);
		}
	
		// Update is called once per frame
		void Update ()
		{

				if (!isDraggingUI && !isPlaying && canMove) {
						Vector3 mousePos = Camera.mainCamera.ScreenToWorldPoint (Input.mousePosition);
						if (Input.GetMouseButton (0)) {
								if (selectedPiece == null) {
										RaycastHit2D hit = Physics2D.Raycast (new Vector2 (mousePos.x, mousePos.y), Vector2.zero);
										if (hit.transform != null) {
												Debug.Log (hit.transform);
												selectedPiece = hit.transform.gameObject;
										}
					
								} else {
										Vector3 pos = getSnappedPosition (mousePos);
										if (!isPositionOccupied (pos)) {
												selectedPiece.transform.position = pos;
												foreach (PieceLocation pl in pieces) {
														if (pl.piece == selectedPiece) {
																pl.position = pos;
																pl.rotation = selectedPiece.transform.rotation;
														}
												}
										}
								}
						} else {
								if (selectedPiece != null) {
										selectedPiece = null;
								}
						}
				}
		}
		
		Vector3 getSnappedPosition (Vector3 pos)
		{
				int x = (int)(pos.x / snapSize) * snapSize;
				int y = (int)(pos.y / snapSize) * snapSize;
				pos.x = x;
				pos.y = y;
				pos.z = 0;
				return pos;
		}

		public void EnterPlayMode ()
		{
				if (!isPlaying) {
						this.gameObject.AddComponent<DestroyBody> ();
						Debug.Log ("playmode");
						foreach (PieceLocation p in pieces) {
								p.piece.rigidbody2D.isKinematic = false;
						}
						isPlaying = true;
				} 
		}

		public void ExitPlayMode ()
		{
				if (isPlaying) {
						Destroy (gameObject.GetComponent<DestroyBody> ());
						foreach (PieceLocation p in pieces) {
								p.piece.rigidbody2D.isKinematic = true;
								p.piece.transform.position = p.position;
								p.piece.transform.rotation = p.rotation;
						}
						isPlaying = false;
				}
		}

		public void DisplaySaveDialog ()
		{
	
				saveInput.SetActive (true);
				canMove = false;
				
		}

		public void HideSaveDialog ()
		{
				saveInput.SetActive (false);
				canMove = true;
		}

		public void Save ()
		{

				string filename = saveName.value;
				if (!ES2.Exists (filename + ".json")) {
						Debug.Log ("File to be saved:" + filename);
						string json = LitJson.JsonMapper.ToJson (SerializePieces ());
						Debug.Log ("JsonData:" + json);
						ES2.Save (json, filename + ".json");
						HideSaveDialog ();
				} else {

				}
				
		}

		public void Load (string file)
		{
				string json = ES2.Load<string> (file + ".json");
				Debug.Log (json);
				foreach (PieceLocation p in pieces) {
						Destroy (p.piece);
				}
				pieces = DeserializePieces (json);
			
		}

		public void InstantiatePiece (GameObject prefab)
		{
				Vector3 finalPos = Camera.mainCamera.ScreenToWorldPoint (Input.mousePosition);
				
				finalPos.z = 0;
				int x = (int)(finalPos.x / 16) * 16;
				int y = (int)(finalPos.y / 16) * 16;
				finalPos.x = x;
				finalPos.y = y;
				if (!isPositionOccupied (finalPos)) {
						Debug.Log ("world pos:" + finalPos);
						if (prefab != null) {
								GameObject p = (GameObject)Instantiate (prefab, finalPos, Quaternion.identity);
								p.rigidbody2D.isKinematic = true;
								PieceLocation pl = new PieceLocation ();
								pl.piece = p;
								pl.position = finalPos;
								pieces.Add (pl);

						}
				}
		}

		public bool isPositionOccupied (Vector3 p)
		{

				RaycastHit2D[] hits = Physics2D.RaycastAll (new Vector2 (p.x, p.y), Vector2.zero);
				foreach (RaycastHit2D hit in hits) {
						if (hit.transform != null && hit.transform.gameObject != selectedPiece) {
								Debug.Log (hit.transform);
								return true;
						}
				}
				return false;
		}

		public SerializablePieceLocation[] SerializePieces ()
		{
				List<SerializablePieceLocation> ps = new List<SerializablePieceLocation> ();
				foreach (PieceLocation pl in pieces) {
						ps.Add (pl.getSerializable ());
				}
				return ps.ToArray ();

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

[Serializable]
public class PieceLocation
{
		public string id;
		public GameObject piece;
		public Vector3 position;
		public Quaternion rotation;

		public PieceLocation ()
		{
				id = Guid.NewGuid ().ToString ();
		}

		public byte[] ObjectToByteArray ()
		{

				BinaryFormatter bf = new BinaryFormatter ();
				MemoryStream ms = new MemoryStream ();
				bf.Serialize (ms, getSerializable ());
				return ms.ToArray ();
		}

		private PieceLocation ByteArrayToObject (byte[] arrBytes)
		{
				MemoryStream memStream = new MemoryStream ();
				BinaryFormatter binForm = new BinaryFormatter ();
				memStream.Write (arrBytes, 0, arrBytes.Length);
				memStream.Seek (0, SeekOrigin.Begin);
				PieceLocation obj = (PieceLocation)binForm.Deserialize (memStream);
				return obj;
		}

		public SerializablePieceLocation getSerializable ()
		{
				SerializablePieceLocation spl = new SerializablePieceLocation ();
				spl.rx = rotation.eulerAngles.x.ToString ();
				spl.ry = rotation.eulerAngles.y.ToString ();
				spl.rz = rotation.eulerAngles.z.ToString ();
				spl.x = position.x.ToString ();
				spl.y = position.y.ToString ();
				spl.z = position.z.ToString ();
				spl.piece_name = piece.name.Replace ("(Clone)", "");
				return spl;

		}

		public void parseSerializable (SerializablePieceLocation spl)
		{
				this.id = spl.id;
				this.position = new Vector3 (float.Parse (spl.x), float.Parse (spl.y), float.Parse (spl.z));
				this.rotation = Quaternion.Euler (float.Parse (spl.rx), float.Parse (spl.ry), float.Parse (spl.rz));
		}
}

[Serializable]
public class SerializablePieceLocation
{
		public string id;
		public string piece_name;
		public string x, y, z;
		public string rx, ry, rz;
}
