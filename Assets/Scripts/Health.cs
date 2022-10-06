using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public enum typeOfHealth
    {
        Player,
        Enemy,
    }
    public typeOfHealth type;
    public float health;
    public float maxHealth;
    private float invincibiltyTime;
    public float invincibleTime;
    public List<GameObject> alreadyHit = new List<GameObject>();
    public TextMeshProUGUI text;
    public bool dead = false;
    public UnityEvent deathEvent;
    public UnityEvent takeDamage;

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
            text = null;
        }
    }

    public bool GetHit(float damage, GameObject source)
    {
        
        if (invincibleTime < Time.time && !dead && this.gameObject!=source)
        {
            health -= damage;
            takeDamage.Invoke();
            if (health <= 0 && !dead)
            {
                dead = true;
                deathEvent.Invoke();
            }
            invincibleTime = Time.time + invincibiltyTime;
            if (this.gameObject.layer == LayerMask.NameToLayer("Enemy") && dead)
            {
                Destroy(this.gameObject);
                return true;
            }
        }
        return false;

    }   


}
