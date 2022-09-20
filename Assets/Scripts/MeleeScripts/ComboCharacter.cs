using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;

    public Collider2D hitbox;

    float touchStartTime = 0f;

    bool cancelled = false;

    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            touchStartTime = Time.time;
        }

        if(touchStartTime != 0 && Time.time - touchStartTime > 3.0f)
        {
            if (meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
            {
                meleeStateMachine.SetNextState(new MeleeHeavyState());
            }
            cancelled = true;
            touchStartTime = 0;
        }

        if(Input.GetMouseButtonUp(0)) {
            float delta = Time.time - touchStartTime;
            touchStartTime = 0;

            if (delta < 1.0f) {
                Debug.Log(meleeStateMachine.CurrentState);
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

            cancelled = false;
        }
    }
}
