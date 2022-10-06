using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrap : MonoBehaviour
{
    public GameObject player;
    public event Action OnTrapDone;
    private TrapStates currentState = TrapStates.OFF;

    private float triggerTimer;
    private float timeActive;
    public float timeToTrigger = 3.0f;
    public float timeTriggered = 3.0f;

    private bool isInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        triggerTimer = timeToTrigger;
        timeActive = timeTriggered;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == TrapStates.READYING)
        {
            triggerTimer -= Time.deltaTime;
            if (triggerTimer <= 0)
            {
                activate();
            }
        }
        else if(currentState == TrapStates.ON)
        {
            timeActive -= Time.deltaTime;
            if (timeActive <= 0)
            {
                deactivate();
            }
        }
    }

    private void activate()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        triggerTimer = timeToTrigger;
        currentState = TrapStates.ON;

        if (isInArea)
        {
            player.GetComponent<Health>().GetHit(1, this.gameObject);
        }
    }

    private void deactivate()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        timeActive = timeTriggered;
        currentState = TrapStates.OFF;
        OnTrapDone?.Invoke();
    }

    //Public Methods
    public TrapStates getCurrentState()
    {
        return currentState;
    }

    //Returns true if trap was started correctly and false otherwise
    public bool triggerTrap()
    {
        if (currentState == TrapStates.OFF)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            currentState = TrapStates.READYING;
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerAttribute>() != null)
        {
            isInArea = true;
            if(currentState == TrapStates.ON)
            {
                player.GetComponent<Health>().GetHit(1, this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInArea = false;
    }
}

public enum TrapStates
{
    OFF,
    READYING,
    ON
}
