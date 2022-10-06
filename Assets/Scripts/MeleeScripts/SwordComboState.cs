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
        Duration = playerAttributes.AttackInfoArray[1].attackDuration;
        comboDelay = playerAttributes.AttackInfoArray[1].comboDelay;
        damage = playerAttributes.AttackInfoArray[1].attackDamage;

        animator.SetTrigger(StateMachine.Instance.weaponName + attackIndex);
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
