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
            try
        {
            if (collision.gameObject.tag == "Player")
            {
                Health shot = collision.gameObject.GetComponent<Health>();
                if(shot != null)
                {
                    shot.GetHit(damage, this.gameObject);
                    Destroy(this.gameObject);
                }

             
            }
            else if (collision.gameObject.tag == "Obstacle") Destroy(gameObject);
        }
            catch (System.Exception)
            {

                throw;
            }


     
    }
}
