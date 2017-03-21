using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject ghost;
	public GameObject panelMenu;
	public GameObject panelStart;
	public Text ghosttext;
	public Text scoreText;
	public List<GhostMove> ghosts = new List<GhostMove> ();

	private float width;
	private int maxghost = 9;
	private int currentghost = 0;
	private int ghostScore = 0;
	private bool startgame = false;
	public bool startSecond = false;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		Vector2 screen = new Vector2 (Screen.width, Screen.height);
		Vector2 pos = Camera.main.ScreenToWorldPoint (screen);
		float bounds = ghost.gameObject.GetComponent<SpriteRenderer> ().bounds.extents.x;
		width = pos.x - bounds;
	}
	
	// Update is called once per frame
	void Update () {
		Spawn ();
		ghosttext.text = currentghost.ToString () + "/10";

		if (Input.GetMouseButtonDown (0)) {
			/*Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D[] col = Physics2D.OverlapPointAll (new Vector2 (temp.x, temp.y), LayerMask.GetMask ("Enemy"));
			foreach (Collider2D c in col) {
				ghostScore+=10;
			}*/
			ClickGhost ();
		}
		scoreText.text = ghostScore.ToString ();

	}

	void ClickGhost(){
		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);
		if (hit!=null && hit.collider!=null && hit.collider.tag == "Enemy") {
			ghostScore += 10;
		}
	}

	void Spawn(){
		if (currentghost < maxghost) {
			GameObject obj = Instantiate (ghost, new Vector2(Random.Range (-width, width),-4.14f), Quaternion.identity) as GameObject;
			currentghost++;
		}
	}


	public void ShowPanel(){
		panelMenu.gameObject.SetActive (true);
	}

	public void Restart(){
		DestroyAllGhost ();
		panelMenu.gameObject.SetActive (false);

		if (ghosts.Count == 0) {
			currentghost = 0;
			ghostScore = 0;
			Spawn ();
		}

	}
	public void GoToMenu(){
		panelMenu.gameObject.SetActive (false);
		panelStart.gameObject.SetActive (true);

	}
	public void StartGame(){
		panelStart.gameObject.SetActive (false);
		DestroyAllGhost ();
		panelMenu.gameObject.SetActive (false);

		if (ghosts.Count == 0) {
			currentghost = 0;
			ghostScore = 0;
			Spawn ();
		}
	}


	public void RegistreGhost(GhostMove ghost){
		ghosts.Add (ghost);
	}
	public void DestroyAllGhost(){
		foreach (GhostMove c in ghosts) {
			Destroy (c.gameObject);
		}
		ghosts.Clear ();
	}

}
