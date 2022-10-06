using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponparent : MonoBehaviour
{
    [Header("Objects")]
    public SpriteRenderer weaponRenderer;
    public SpriteRenderer charaterRenderer;
    public Health health;

    [Header("Input")]
    public PlayerMovement movement;

    private void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Before first Scene loaded");
    }

    public bool canRotate = true;
    private void Update()
    {
        if (health.dead)
        {
            return;
        }
        if (canRotate)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
        }

        //   Vector2 scale = transform.localScale;
        //   if (Mathf.Abs(rotation_z) > 90)
        //   {
        //       scale.y = -1;
        //   }
        //   else if (Mathf.Abs(rotation_z) < 90)
        //   {
        //       scale.y = 1;
        //   }
        //   transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = charaterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = charaterRenderer.sortingOrder - 1;
        }
    }
}

