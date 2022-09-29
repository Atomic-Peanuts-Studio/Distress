using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    public float resourceConsumption = 25;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(ConsumeResource())
            {
                Shoot();
            }
        }
    }

    void Shoot()
    { 
        Vector2 direction = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletForce;
    }

    private bool ConsumeResource()
    {
        float currentMana = this.GetComponent<PlayerAttribute>().mana;
        if (currentMana - resourceConsumption >= 0)
        {
            this.GetComponent<PlayerAttribute>().mana = currentMana - resourceConsumption;
            return true;
        }

        return false;
    }
}
