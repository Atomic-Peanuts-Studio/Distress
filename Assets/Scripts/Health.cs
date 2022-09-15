using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public enum typeOfHealth
    {
        Player,
        Enemy,
    }
    public typeOfHealth type;
    public int health;
    public int maxHealth;
    public float invincibiltyTime;
    public float invincibleTime;
    public List<GameObject> alreadyHit = new List<GameObject>();
    public TextMeshProUGUI text;
    public bool dead = false;


    // Start is called before the first frame update
    void Start()
    {
        if (type == typeOfHealth.Player)
        {
            invincibiltyTime = 0.5f;
            text.text = (health + "/" + maxHealth);
        }
        else
        {
            invincibiltyTime = 0.2f;
        }
    }

    public bool GetHit(int damage, GameObject source)
    {
            if (source.tag == "EnemyDamage" && invincibleTime < Time.time && !dead)
            {
                health -= damage;
                text.text = (health + "/" + maxHealth);
                if (health <= 0 && !dead)
                {
                    dead = true;
                    text.text = "You Died";
                }
                invincibleTime = Time.time + invincibiltyTime;
                return true;
            }
        else if(source.tag == "PlayerDamage")
        {
            health -= damage;
            if (health <=0 )
            {
                Destroy(this.gameObject);
            }
            return true;
        }
            return false;

    }

}
