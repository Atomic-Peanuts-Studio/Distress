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
    private SpriteRenderer sprite;
    public float screenShakeAmount = 0;


    // Start is called before the first frame update
    void Start()
    {
        takeDamage.AddListener(ShakeCamera);
        sprite = this.GetComponent<SpriteRenderer>();
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
          //  StartCoroutine(ShakeOnHit());

        }
    }
    public void ShakeCamera()
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
