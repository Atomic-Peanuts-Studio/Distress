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
        Duration = playerAttributes.duration[1];
        comboDelay = playerAttributes.comboDelay[1];
        damage = playerAttributes.basicAttackDamage[1];

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
