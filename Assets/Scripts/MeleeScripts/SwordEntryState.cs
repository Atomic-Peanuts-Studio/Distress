using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Start Attacking

        attackIndex = 1;
        Duration = 0.5f;
        animator.SetTrigger("Attack"+attackIndex);
        Debug.Log("Player Attack" + attackIndex + " Fired!");
    }
    public override void OnUpdate()
    {
        if (fixedtime >= Duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new SwordComboState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
        base.OnUpdate();
    }
}
