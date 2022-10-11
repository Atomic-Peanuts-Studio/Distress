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
        Duration = playerAttributes.AttackInfoArray[0].attackDuration;
        comboDelay = playerAttributes.AttackInfoArray[0].comboDelay;
        damage = playerAttributes.AttackInfoArray[0].attackDamage;

        animator.SetTrigger(StateMachine.Instance.weaponName+attackIndex);
        Debug.Log("Player Attack" + attackIndex + " Fired!");
        Debug.Log(StateMachine.Instance.weaponName.ToString());
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
