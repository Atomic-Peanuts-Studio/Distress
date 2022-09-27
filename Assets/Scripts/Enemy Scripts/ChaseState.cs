using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    [SerializeField] private float _attackRange;

    private void OnEnable()
    {
        Type = StateType.Chase;
    }
    public override void DoUpdate()
    {
        if (_owner.targetedPlayer && Vector2.Distance(_owner.targetedPlayer.position, _owner.enemyRoot.position) > _attackRange)
        {
            _owner.movement.MoveTowards(_owner.targetedPlayer.position);
            //_owner.enemyRoot.rotation = Quaternion.LookRotation(Vector3.forward, _owner.targetedPlayer.position);
        }
        else
        {
            _owner.movement.StandStill();
            _owner.ChangeState(nextState);
        }
    }
}
