using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHeavyState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        damage = 50;
        Duration = 1f;
        animator.SetTrigger("HeavyAttack");
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
