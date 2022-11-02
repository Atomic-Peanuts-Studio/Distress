using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingIdleState : EnemyState
{
    [SerializeField] private float _spotRange;
    private void OnEnable()
    {
        Type = StateType.Idle;
    }

    public override void DoUpdate()
    {
        if (Vector2.Distance(_owner.enemyRoot.position, _owner.targetedPlayer.position) < _spotRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(_owner.enemyRoot.position, _owner.targetedPlayer.position - _owner.enemyRoot.position);
            if (hit.collider != null) _owner.ChangeState(nextState);
        }
    }
}
