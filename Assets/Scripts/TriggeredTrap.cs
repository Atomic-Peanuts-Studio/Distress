using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredTrap : MonoBehaviour
{
    public GameObject trigger;
    public SpriteRenderer sprite;
    public Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger.GetComponent<TrapTrigger>().isActive)
        {
            sprite.color = Color.black;
        }   
        else
        {
            sprite.color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(trigger.GetComponent<TrapTrigger>().isActive)
        {
            //do damage
        }
    }
}
