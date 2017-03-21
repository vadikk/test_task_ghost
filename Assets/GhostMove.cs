using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour {

	private float speed = 0f;

	// Use this for initialization
	void Start () {
		speed = Random.Range (0.3f, 1.3f);
		GameManager.instance.RegistreGhost (this);
		//print (speed);
	}
	
	// Update is called once per frame
	void Update () {
		MoveGhost ();
	}

	void MoveGhost(){
		
		transform.position += Vector3.up*speed*Time.deltaTime;
	}


}
