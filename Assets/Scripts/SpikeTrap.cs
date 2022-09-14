using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public SpriteRenderer sprite;
    public float interval = 5.0f;
    private float timer;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            changeState();
        }
    }

    public void changeState()
    {
        isActive = !isActive;
        timer = interval;

        if (isActive)
        {
            sprite.color = Color.black;
        }
        else { sprite.color = Color.white; }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(isActive)
        {
            //GameObject item = collision.gameObject;    //deal damage
            //item.GetComponent<health>.health -= 5;
        }
    }
}
