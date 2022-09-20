using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFinishState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Start Attacking

        attackIndex = 3;  
        Duration = 1f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack" + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        if (fixedtime >= Duration)
        {
          stateMachine.SetNextStateToMain();

        }
        base.OnUpdate();


    }
}
