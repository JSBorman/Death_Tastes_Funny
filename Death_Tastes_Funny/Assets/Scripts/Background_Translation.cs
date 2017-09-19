using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Translation : MonoBehaviour {

	public GameObject player;
	public Camera camera;

	//Parallaxing variables
	public float upper_bound = 100f;
	public float lower_bound = -100f;
	public float move_rate;

	Vector3 player_last_pos;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		Sprite sprite = this.GetComponent<Sprite> ();
		//Don't change objects when outside of viewport
		Vector3 viewPos = camera.WorldToViewportPoint(this.transform.position);
		if ( viewPos.x < 0 || viewPos.x > 1)
			return;

		parralaxing ();
	}

	public void parralaxing(){
		float new_pos = 0.0f;

		//Player moved left
		if (player_last_pos.x >= player.transform.position.x && this.transform.position.y > lower_bound) {
			new_pos = move_rate * -1;
		}

		//Player moved right
		else if (player_last_pos.x <= player.transform.position.x && this.transform.position.y < upper_bound) {
			new_pos = move_rate;
		}

		//If the player moved, transform the background piece
		if(player_last_pos.x != player.transform.position.x)
			this.transform.Translate (0, new_pos, 0);

		player_last_pos = player.transform.position;
	}
}
