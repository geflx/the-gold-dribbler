using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundCheck : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.tag == "Ground") {
            Player.instance.jumpCounter = Player.instance.maxJumps;
            Player.instance.isGrounded = true;
        }
        else if(collider.tag == "Teleporter") {
            NextHordeTrigger.instance.OnTriggerPortal();
        }
    }

    public void OnTriggerExit2D (Collider2D collider)
    {
        if(collider.tag == "Ground")
            Player.instance.isGrounded = false;
    }
}
