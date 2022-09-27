using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionState : EnemyState
{
    [SerializeField] private float _repositioningDistance;
    private void OnEnable()
    {
        Type = StateType.Reposition;
    }
    public override void DoUpdate()
    {
        if (_owner.currentAttackCooldown > 0.1f)
        {
            if (Vector2.Distance(_owner.targetedPlayer.position, _owner.enemyRoot.position) < _repositioningDistance)
            {
                var positionAwayFromPlayer = _owner.enemyRoot.position - _owner.targetedPlayer.position;
                positionAwayFromPlayer.Normalize();
                positionAwayFromPlayer *= _repositioningDistance;
                _owner.movement.MoveTowards(positionAwayFromPlayer);
                _owner.enemyRoot.rotation = Quaternion.LookRotation(Vector3.forward, _owner.targetedPlayer.position);
            }
        }
        else _owner.ChangeState(NextState);
    }
}
