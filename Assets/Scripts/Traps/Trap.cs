using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float interval = 2.0f;
    private float timer;
    public bool isActive = false;
    private bool isInArea = false;
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            changeState();
        }
    }

    public void changeState()
    {
        isActive = !isActive;
        timer = interval;
        if (isActive)
        {
            sprite.color = Color.black;

            if(isInArea)
            {
                DealDamage();
                
            }
        }
        else { 
            sprite.color = Color.white; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerAttribute>() != null)
        {
            isInArea = true;

            if (player == null)
            {
                player = collision.gameObject;
            }
            if(isActive)
            {
                DealDamage();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInArea = false;
    }

    private void DealDamage()
    {
        player.GetComponent<Health>().GetHit(1, this.gameObject);
    }
}
