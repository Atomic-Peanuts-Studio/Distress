using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredTrap : MonoBehaviour
{
    public SpriteRenderer sprite;
    private List<GameObject> inTrap = new List<GameObject>();
    public bool triggered = false;
    private bool damageDealt = false;

    public float interval = 2.0f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                deactivateTrap();
            }
        }
    }

    public void activateTrap()
    {
        sprite.color = Color.black;
        
        if(inTrap.Count > 0 && !damageDealt)
        {
            for (int i = 0; i < inTrap.Count - 1; i++)
            {
                DealDamage(inTrap[i]);
            }

            damageDealt = true;
        }
    }

    public void deactivateTrap()
    {
        triggered = false;
        sprite.color = Color.white;
        timer = interval;
        damageDealt = false;
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inTrap.Add(collision.gameObject);
        if(triggered)
        {
            DealDamage(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrap.Remove(collision.gameObject);
    }

    private void DealDamage(GameObject damaged)
    {
        damaged.GetComponent<Health>().GetHit(1, this.gameObject);
    }
}
