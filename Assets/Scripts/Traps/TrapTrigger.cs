using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public bool isActive = false;
    public GameObject trap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.name == "Player")
        {
            trap.GetComponent<TriggeredTrap>().triggered = true;
            trap.GetComponent<TriggeredTrap>().activateTrap();
        }
    }
}
