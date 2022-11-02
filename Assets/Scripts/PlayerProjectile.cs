using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int bulletDamage = 25;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Health>().GetHit(bulletDamage, this.gameObject);
        }
        else if(collision.gameObject.layer == 10 || collision.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
