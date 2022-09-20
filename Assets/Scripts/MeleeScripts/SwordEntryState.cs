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
        comboDelay = 2.8f;
        damage = 25;
        animator.SetTrigger("Attack"+attackIndex);
        Debug.Log("Player Attack" + attackIndex + " Fired!");
    }
    public override void OnUpdate()
    {

        if (fixedtime >= Duration)
        {
            if (fixedtime >= comboDelay)
            {
                stateMachine.SetNextStateToMain();
            }
            if (shouldCombo)
            {
                stateMachine.SetNextState(new SwordComboState());
            }
        }
        base.OnUpdate();
    }
}
