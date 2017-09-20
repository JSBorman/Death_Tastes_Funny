using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

    private NPC collidingWith;
    bool interacting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!interacting && collidingWith != null && Input.GetKeyDown(KeyCode.F)) {
            SetPlayerMovementEnabled(false);
            interacting = true;
            collidingWith.Interact(this);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        collidingWith = collision.GetComponent<NPC>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<NPC>() == collidingWith) {
            collidingWith = null;
        }
    }

    public void endInteraction() {
        interacting = false;
        SetPlayerMovementEnabled(true);
    }

    public void SetPlayerMovementEnabled(bool enabled) {
        GetComponent<Player_Movement>().enabled = enabled;
    }


}
