using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToSameY : MonoBehaviour
{

    public GameObject teleportLocation;
    public bool isTrigger = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = new Vector3(teleportLocation.transform.position.x, other.transform.position.y, 0);
        if (isTrigger) {
            GameManager.INSTANCE.RefreshNPCs();
        }
    }
}