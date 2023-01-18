using System;
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
    [Header("Type")]
    public typeOfHealth type;
    [Header("Health")]
    [DrawIf("type", typeOfHealth.Enemy,DrawIfAttribute.DisablingType.DontDraw)]
    public float health;
    [DrawIf("type", typeOfHealth.Enemy,DrawIfAttribute.DisablingType.DontDraw)]
    public float maxHealth;
    private float invincibiltyTime;
    [Header("Invincibility")]
    public float invincibleTime;
    [NonSerialized]
    public bool dead = false;
    [Header("Events")]
    public UnityEvent deathEvent;
    public UnityEvent<float> takeDamage;
    public event Action<float> onHealedForAmount;
    public event Action<float> healthChanged;
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite;
    [Header("Screenshake")]
    public float screenShakeAmount = 0;


    // Start is called before the first frame update
    void Start()
    {
        takeDamage.AddListener(ShakeCamera);
        if (type == typeOfHealth.Player)
        {
            invincibiltyTime = PlayerAttribute.instance.invincibleTime;
            maxHealth= PlayerAttribute.instance.maxHealth;
            health= PlayerAttribute.instance.health;

        }
        else
        {
            invincibiltyTime = 0.2f;
        }
       

    }
    public void UpdatePlayerValuesHTA()
    {
        PlayerAttribute.instance.health = health;
        PlayerAttribute.instance.maxHealth = maxHealth;
    }
    public void UpdatePlayerValuesATH()
    {
        maxHealth = PlayerAttribute.instance.maxHealth;
    }
    public IEnumerator DamageFlicker()
    {

            sprite.color = new Color(1f, 1f, 1f, .5f);
            yield return new WaitForSeconds(invincibiltyTime/2.5f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(invincibiltyTime / 2.5f);
        
    }
    public void Update()
    {
        if (invincibleTime > Time.time)
        {
            StartCoroutine(DamageFlicker());
            StartCoroutine(ShakeOnHit());

        }
    }
    public void ShakeCamera(float damageAmount)
    {
        if (type == typeOfHealth.Player)
        {
            CameraShake.instance.ShakeCamera(screenShakeAmount, invincibiltyTime);

        }
        else
        {
            CameraShake.instance.ShakeCamera(2, invincibiltyTime);
        }

    }

    public IEnumerator ShakeOnHit()
    {
        transform.localPosition += new Vector3(0.02f, 0, 0);
        yield return new WaitForSeconds(0.01f);
        transform.localPosition -= new Vector3(0.04f, 0, 0);
        yield return new WaitForSeconds(0.01f);
        transform.localPosition += new Vector3(0.02f, 0, 0);
        yield return new WaitForSeconds(0.01f);

    }
    public bool GetHit(float damage, GameObject source)
    {

        if (invincibleTime < Time.time && !dead && this.gameObject != source && Input.GetKey(KeyCode.E) == false)
        {
            health -= damage;
            if (type == typeOfHealth.Player)
            {
                UpdatePlayerValuesHTA();
            }
            takeDamage.Invoke(damage);
            healthChanged?.Invoke(health);
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
        else if (Input.GetKey(KeyCode.E) == true) // Player Blocking
        {
            Debug.Log("Damage Blocked");
            return false;
        }
        return false;

    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        onHealedForAmount?.Invoke(amount);
        healthChanged.Invoke(health);
    }
}
