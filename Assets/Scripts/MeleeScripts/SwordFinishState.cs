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
        Duration = playerAttributes.AttackInfoArray[2].attackDuration;
        comboDelay = playerAttributes.AttackInfoArray[2].comboDelay;
        damage = playerAttributes.AttackInfoArray[2].attackDamage;

        animator.SetTrigger(weaponName + attackIndex);
        Debug.Log("Player Attack" + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (fixedtime >= Duration)
        {
          stateMachine.SetNextStateToMain();

        }

    }
}
