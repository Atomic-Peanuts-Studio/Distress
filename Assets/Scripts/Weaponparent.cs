using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponparent : MonoBehaviour
{

    public SpriteRenderer weaponRenderer;
    public SpriteRenderer charaterRenderer;
    public Animator animator;
    public float delay = 0.15f;
    private bool attackBlocked = false;

    public float nextAttack = 0.15f;
    public float cooldown = 1f;
    public Health health;

    private void Update()
    {
        if (health.dead)
        {
            return;
        }
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        Vector2 scale = transform.localScale;

        if (Mathf.Abs(rotation_z) > 90)
        {
            scale.y = -1;
        }
        else if (Mathf.Abs(rotation_z) < 90)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if (transform.eulerAngles.z>0 && transform.eulerAngles.z<180)
        {
            weaponRenderer.sortingOrder = charaterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = charaterRenderer.sortingOrder - 1;
        }
        if (Input.GetMouseButton(0) && nextAttack < Time.time)
        {
            Attack();
        }
    }

    public void Attack()
    {
  
        animator.SetTrigger("Attack");
        attackBlocked = true;
        nextAttack = Time.time + cooldown;
    }

}

