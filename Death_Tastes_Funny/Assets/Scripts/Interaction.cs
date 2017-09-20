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
            collidingWith.Interact(this);
            interacting = true;
        } else if (interacting) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                collidingWith.Select(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                collidingWith.Select(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                collidingWith.Select(2);
            }
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
    }


}
