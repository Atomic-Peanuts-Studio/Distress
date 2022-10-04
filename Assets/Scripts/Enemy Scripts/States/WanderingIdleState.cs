using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingIdleState : EnemyState
{
    [SerializeField] private float _spotRange = 2.5f;
    [SerializeField] private float _maxRepositionTimer = 2f;
    private float _currentIdleRepositionTimer;
    private void OnEnable()
    {
        Type = StateType.Idle;
    }
    public override void DoStart()
    {
        _currentIdleRepositionTimer = _maxRepositionTimer;
    }
    public override void DoUpdate()
    {
        _currentIdleRepositionTimer -= Time.deltaTime;
        if (_currentIdleRepositionTimer < 0.1f) _owner.movement.MoveToRandomLocation(5f);
        if (Vector2.Distance(_owner.enemyRoot.position, _owner.targetedPlayer.position) < _spotRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(_owner.enemyRoot.position, _owner.targetedPlayer.position - _owner.enemyRoot.position);
            if (hit.collider != null) _owner.ChangeState(nextState);
        }
    }
}
