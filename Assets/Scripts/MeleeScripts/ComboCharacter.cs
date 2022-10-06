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
   

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
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
        float delta = Time.time - touchStartTime;
        
        if (health.dead == true)
        {
            return;
        }
        if(movement.controls.Player.Melee.WasPressedThisFrame())
        {
            touchStartTime = Time.time;
            firstRun = true;
        }
        if (firstRun)
        {
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
        

        if (touchStartTime != 0 && Time.time - touchStartTime > 3.0f)
        {
            cancelled = true;
            touchStartTime = 0;
            if (meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
            {

                meleeStateMachine.SetNextState(new MeleeHeavyState());
            }

        }

        if(movement.controls.Player.Melee.WasReleasedThisFrame()) {
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
