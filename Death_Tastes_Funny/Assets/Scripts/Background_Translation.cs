using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Translation : MonoBehaviour {

	private GameObject player;
	private Camera camera;

	//Parallaxing variables
	public float par_upper_bound = 100f;
	public float par_lower_bound = -100f;
	public float par_move_rate;

	//Squashing Variables
	public float squa_upper_bound = 2.0f;
	public float squa_lower_bound = 0.5f;
	public float squa_move_rate = 1.0f;
	public List<int> squa_allowed_formats = new List<int> {1, 1, 1};	//All formats allowed
	private float squa_transform = 1.0f;
	private float squa_time_since_format = 1.0f;	//1 second between direction changes
	private int squa_toShrink = 1;
	private int squa_format = 0;

	Vector3 player_last_pos;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();

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
		squa_time_since_format -= Time.deltaTime; //Update time

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

	//Over time, stretch and squish the object over different vertices
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

		//If timer ran out
		if (squa_time_since_format <= 0) {
			squa_time_since_format = 1.0f;
			squa_format = Random.Range (0, 2);
		}

		//If selected format is not allowed, stop moving
		if (squa_allowed_formats[squa_format] == 0)
			squa_format = 0;

		//Alter scale of planet based on current orientation
		switch (squa_format) {
			case 0:
				this.transform.localScale += new Vector3 (change, change, change);
				break;	
			case 1:
				this.transform.localScale += new Vector3 (0, change, 0);
				break;
			case 2:
				this.transform.localScale += new Vector3 (change, 0, 0);
				break;
			default:
				this.transform.localScale += new Vector3 (0, 0, 0);
				break;
		}
	}
}
