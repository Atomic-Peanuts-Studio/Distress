using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionState : EnemyState
{
    [SerializeField] private float _repositioningDistance;
    [SerializeField] private float _rotationSpeed = 0.5f;
    private bool _moveClockwise;
    private float _angle;
    private void OnEnable()
    {
        Type = StateType.Reposition;
    }
    public override void DoStart()
    {
        base.DoStart();
        _moveClockwise = Random.value > 0.5f;
        _angle = 1f;
    }
    public override void DoUpdate()
    {
        if (_owner.meleeAttackCooldown > 0.1f || _owner.rangedAttackCooldown > 0.1f)
        {
            float distanceToPlayer = Vector2.Distance(_owner.targetedPlayer.position, _owner.enemyRoot.position);
            if (distanceToPlayer < _repositioningDistance + 0.5f && distanceToPlayer > _repositioningDistance - 0.5f) CircleAroundPlayer();
            else if (distanceToPlayer < _repositioningDistance) BackAwayFromPlayer();
        }
        else
        {
            _owner.movement.StandStill();
            _owner.ChangeState(nextState);
        }
    }
    private void BackAwayFromPlayer()
    {
        var positionAwayFromPlayer = _owner.enemyRoot.position - _owner.targetedPlayer.position;
        positionAwayFromPlayer.Normalize();
        positionAwayFromPlayer *= _repositioningDistance;
        _owner.movement.MoveTowards(positionAwayFromPlayer);
    }
    private void CircleAroundPlayer()
    {
        Vector3 posOffset;
        if (!_moveClockwise) posOffset = new Vector3(Mathf.Cos(_angle) * _repositioningDistance, Mathf.Sin(_angle) * _repositioningDistance, 0);
        else posOffset = new Vector3(Mathf.Sin(_angle) * _repositioningDistance, Mathf.Cos(_angle) * _repositioningDistance, 0);
        _owner.movement.MoveTowards(_owner.targetedPlayer.position + posOffset);
        _angle += Time.deltaTime * _rotationSpeed;
    }
}
