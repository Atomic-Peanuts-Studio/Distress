using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordComboState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Combo into 2nd attack

        attackIndex = 2;
        Duration = 0.2f;
        comboDelay = 3.0f;
        damage = 25;
        animator.SetTrigger("Attack" + attackIndex);
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
                stateMachine.SetNextState(new SwordFinishState());
            }
        }
        base.OnUpdate();


    }
}
