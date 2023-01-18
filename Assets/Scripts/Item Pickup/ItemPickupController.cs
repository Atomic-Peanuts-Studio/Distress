using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupController : MonoBehaviour
{
    [Header("Variables")]
    public PlayerAttribute Damage;
    public PlayerMovement Speed;
    public ScytheSwipe SlimeDmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (this.gameObject.tag == "Damage")
            {
                Debug.Log("Damage Multiplied");
                Damage.AttackInfoArray[0].attackDamage = 50;
                Damage.AttackInfoArray[1].attackDamage = 50;
                Damage.AttackInfoArray[2].attackDamage = 70;
            }
            else if(this.gameObject.tag == "Speed")
            {
                Debug.Log("Speed Multiplied");
                Speed.moveSpeed = 10;
            }
            else if (this.gameObject.tag == "Corruption")
            {
                Debug.Log("Pain Multiplied");
                SlimeDmg._damage = 5;
            }
            Destroy(this.gameObject);
        }
    }
}
