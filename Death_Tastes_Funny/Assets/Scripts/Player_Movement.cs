using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

	public float move_speed = 10.0f;
    public float jumpForce = 10f;
    public bool isGrounded;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		get_input ();
	}

	public void get_input(){
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * move_speed;
		var y = Input.GetAxis("Vertical") * Time.deltaTime * move_speed;

		var old_pos = transform.position;
		transform.Translate (x, y, 0);

		//OSC SEND
		if (old_pos != transform.position) {
			float move = 1.0f;
			//OSCHandler.Instance.SendMessageToClient ("SuperCollider", "/move", move);
		}

        if (Input.GetKeyDown(KeyCode.W) & isGrounded)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
        }


    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
