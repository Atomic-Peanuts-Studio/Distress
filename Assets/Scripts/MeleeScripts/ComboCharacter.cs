using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    [Header("Input")]
    public PlayerMovement movement;

    private StateMachine meleeStateMachine;
    public float chargeStage = 0;

    public Collider2D hitbox;

    float touchStartTime = 0f;

    bool cancelled = false;

    protected Animator animator;

    protected Health health;
    protected ParticleSystem particle;
    protected ParticleSystem.MainModule mainModule;
    private bool firstRun = false;
    private float delta = 0f;
    private bool chargingHeavy = false;
   

    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        particle = GetComponent<ParticleSystem>();
        mainModule = particle.main;
        mainModule.startLifetime = 0;
        mainModule.startSpeed = 0.15f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!firstRun)
        {
            touchStartTime = 0;
            delta = 0;
        }


        if (health.dead == true)
        {
            return;
        }
        if(movement.controls.Player.Melee.WasPressedThisFrame())
        {
            touchStartTime = Time.time;
            firstRun = true;
            chargingHeavy = true;
        }
        if (firstRun && chargingHeavy)
        {
            movement.chargingHeavy = true;
            particle.Play();
            delta = Time.time - touchStartTime;
            if (delta >= 0.8f)
            {
                mainModule.startLifetime = 0.1f;
                mainModule.startSpeed = 0.15f;
            }
            if (delta >= 1.5f)
            {
                mainModule.startSpeed = 0.45f;
            }
            if (delta >= 2.5f)
            {
                mainModule.startSpeed = 1.45f;
            }
            if (delta >= 3f)
            {
                mainModule.startLifetime = 0;
            }
        }
        else
        {
            movement.ChangeAttackingState(false);
            delta = 0;
            mainModule.startLifetime = 0;
            mainModule.startSpeed = 0f;
            particle.Stop();
        }
        

        if (touchStartTime != 0 && Time.time - touchStartTime > 3.0f)
        {

            chargingHeavy = false;
            movement.chargingHeavy = false;
            cancelled = true;
            touchStartTime = 0;
            if (meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
            {
                meleeStateMachine.SetNextState(new MeleeHeavyState());
            }
    
        }

        if(movement.controls.Player.Melee.WasReleasedThisFrame()) {
            chargingHeavy = false;
            movement.chargingHeavy = false;
            touchStartTime = 0;
            cancelled = false;

            if (delta < 1.0f) {
                if (meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
                {
                    meleeStateMachine.SetNextState(new SwordEntryState());
                }
            }
            else if(delta > 1.0f && !cancelled) {
                if (meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
                {
                    meleeStateMachine.SetNextState(new MeleeHeavyState());
                }
            }


        }
    }
}
