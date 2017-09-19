using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Translation : MonoBehaviour {

	public GameObject player;
	public Camera camera;

	//Parallaxing variables
	public float par_upper_bound = 100f;
	public float par_lower_bound = -100f;
	public float par_move_rate;

	//Squashing Variables
	public float squa_upper_bound = 2.0f;
	public float squa_lower_bound = 0.5f;
	public float squa_move_rate = 1.0f;
	private float squa_transform = 1.0f;
	private int squa_toShrink = 1;

	Vector3 player_last_pos;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		//Always squash
		squashing ();

		Sprite sprite = this.GetComponent<Sprite> ();
		//Don't change objects when outside of viewport
		Vector3 viewPos = camera.WorldToViewportPoint(this.transform.position);
		if ( viewPos.x < 0 || viewPos.x > 1)
			return;

		parralaxing ();
	}

	//Move up & down as the player walks by
	public void parralaxing(){
		float new_pos = 0.0f;

		//Player moved left
		if (player_last_pos.x >= player.transform.position.x && this.transform.position.y > par_lower_bound) {
			new_pos = par_move_rate * -1;
		}

		//Player moved right
		else if (player_last_pos.x <= player.transform.position.x && this.transform.position.y < par_upper_bound) {
			new_pos = par_move_rate;
		}

		//If the player moved, transform the background piece
		if(player_last_pos.x != player.transform.position.x)
			this.transform.Translate (0, new_pos, 0);

		player_last_pos = player.transform.position;
	}

	//Over time, stretch and squish the object
	public void squashing(){

		if (squa_transform > squa_upper_bound) {
			squa_toShrink = -1;
		}

		else if(squa_transform < squa_lower_bound){
			squa_toShrink = 1;
		}

		//Get the amount to change & update our current size
		float change = squa_move_rate * Time.deltaTime * squa_toShrink;
		squa_transform += change;

		this.transform.localScale += new Vector3 (change, change, 0);
	}
}
