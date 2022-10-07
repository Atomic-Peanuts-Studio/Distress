using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrap : MonoBehaviour
{
    private GameObject player = null;
    public event Action OnTrapDone;
    private TrapStates currentState = TrapStates.OFF;

    private float triggerTimer;
    private float timeActive;
    public float timeToTrigger = 3.0f;
    public float timeTriggered = 3.0f;

    private bool isInArea = false;

    public Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        _animator.SetTrigger("SpikesOff");
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
        //this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        _animator.SetTrigger("SpikesTriggered");
        triggerTimer = timeToTrigger;
        currentState = TrapStates.ON;

        if (isInArea)
        {
            player.GetComponent<Health>().GetHit(1, this.gameObject);
        }
    }

    private void deactivate()
    {
        //this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        _animator.SetTrigger("SpikesOff");
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
            //this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            _animator.SetTrigger("SpikesMid");
            currentState = TrapStates.READYING;
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerAttribute>() != null)
        {
            Debug.Log("Entered");
            if(player == null)
            {
                player = collision.gameObject;
                Debug.Log("Player not null anymore");
            }

            isInArea = true;
            if(currentState == TrapStates.ON)
            {
                Debug.Log("Damage should happen now");
                player.GetComponent<Health>().GetHit(1, this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerAttribute>() != null)
        {
            isInArea = false;
        }
    }
}

public enum TrapStates
{
    OFF,
    READYING,
    ON
}
