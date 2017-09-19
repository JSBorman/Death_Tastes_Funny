using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Translation : MonoBehaviour {

	public GameObject player;
	public GameObject background_piece;
	public Camera our_camera;
	public float move_rate;

	Vector3 player_pos;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		bool player_moved = false;
		float new_pos = 0.0f;

		if (player_pos.x != player.transform.position.x) {
			player_moved = true;
		}

		if (player_moved) {
			new_pos = move_rate / Random.Range (1, 5);
		}

		//Check against upper bound of camera
		if (our_camera.pixelRect.y <= background_piece.transform.position.y + our_camera.pixelHeight) {
			new_pos = new_pos * -1;				
		}

		//Lower Bound of camera
		else if (our_camera.pixelRect.min.y >= background_piece.transform.position.y) {
			new_pos = new_pos * -1;
		}
				
		background_piece.transform.Translate (0, new_pos, 0);

		player_pos = player.transform.position;
	}
}
