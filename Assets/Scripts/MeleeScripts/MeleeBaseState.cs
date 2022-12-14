using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEditor.Progress;

public class MeleeBaseState : State
{
    [Header("Input")]
    public PlayerMovement movement;

    public float damage = 25;
    public float comboDelay;
    public float Duration;
    public int nrOfAttacks =1;
    protected Animator animator;
    protected bool shouldCombo;
    protected int attackIndex;
    protected GameObject Player;
    protected PlayerAttribute playerAttributes;
    private StateMachine stateMachine;

    protected Collider2D hitCollider;
    private List<Collider2D> collidersDamaged;
    private float AttackPressedTimer = 0;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        stateMachine = _stateMachine;
        animator = GetComponent<Animator>();
        Player = GetComponent<Animator>().gameObject;
        collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<ComboCharacter>().GetComponentInChildren<BoxCollider2D>();
        playerAttributes = GetComponent<PlayerAttribute>();
        movement = GetComponent<PlayerMovement>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        AttackPressedTimer -= Time.deltaTime;

        if (animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }

        if (movement.controls.Player.Melee.WasPressedThisFrame())

        {
            AttackPressedTimer = 2;
        }

        if (AttackPressedTimer > 0)
        {
            shouldCombo = true;
        }
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[11];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.layerMask = LayerMask.GetMask("Enemy");
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                string tag = collidersToDamage[i].tag;
                // Only check colliders with a valid Team Componnent attached
                if (tag == "Enemy" && tag != "Player")
                {
                    Debug.Log("Enemy Has Taken:" + damage +" Damage From the "+ attackIndex + " Attack");
                    collidersDamaged.Add(collidersToDamage[i]);
                    bool isDead = collidersToDamage[i].GetComponent<Health>().GetHit(damage, Player);

                    
                    if(stateMachine.CurrentState is MeleeHeavyState && isDead)
                    {
                        Player.GetComponent<PlayerAttribute>().addMana();
                    }
                }
            }
        }
    }

    protected void HeavyAttack()
    {
        Collider2D[] collidersToDamage = new Collider2D[11];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.layerMask = LayerMask.GetMask("Enemy");
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {
            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                string tag = collidersToDamage[i].tag;
                // Only check colliders with a valid Team Componnent attached
                if (tag == "Enemy" && tag != "Player")
                {
                    Debug.Log("Enemy Has Taken:" + damage + " Damage From the " + attackIndex + " Attack");
                    collidersDamaged.Add(collidersToDamage[i]);
                    collidersToDamage[i].GetComponent<Health>().GetHit(damage, Player);
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
