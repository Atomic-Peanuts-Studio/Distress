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
        Duration = playerAttributes.duration[0];
        comboDelay = playerAttributes.comboDelay[0];
        damage = playerAttributes.basicAttackDamage[0];

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
