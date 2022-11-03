using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingState : EnemyState
{
    [SerializeField] private float _fleeDistance = 5f;
    public override void DoUpdate()
    {
        float distanceToPlayer = Vector2.Distance(_owner.targetedPlayer.position, _owner.enemyRoot.position);
        if (distanceToPlayer < _fleeDistance)
        {
            var positionAwayFromPlayer = _owner.enemyRoot.position - _owner.targetedPlayer.position;
            positionAwayFromPlayer.Normalize();
            positionAwayFromPlayer *= _fleeDistance;
            _owner.movement.MoveTowards(positionAwayFromPlayer);
        }
        else _owner.ChangeState(nextState);
    }
}
