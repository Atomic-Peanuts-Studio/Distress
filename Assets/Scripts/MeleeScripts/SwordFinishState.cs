using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFinishState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Start Attacking
        Duration = playerAttributes.AttackInfoArray[2].attackDuration;
        comboDelay = playerAttributes.AttackInfoArray[2].comboDelay;
        damage = playerAttributes.AttackInfoArray[2].attackDamage;

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
