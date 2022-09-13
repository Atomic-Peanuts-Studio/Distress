using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag); 
        if (collision.gameObject.tag == "Player")
        {
            bool hit = collision.gameObject.GetComponent<Health>().GetHit(damage, this.gameObject);
            print("has been:"+hit);
            if (hit)
            {
            Destroy(this.gameObject);
            }
            else
            {

            }
        }
    }
}
