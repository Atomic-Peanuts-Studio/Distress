using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAttack : EnemyState
{
    [SerializeField] private float _maxBeforeAttackCooldown = 2f;
    [SerializeField] private float _maxAfterAttackCooldown = 2f;
    [SerializeField] private GameObject _attackPrefab;
    private float _currentBeforeAttackCooldown;
    private void OnEnable()
    {
        Type = StateType.MeleeAttack;
    }
    public override void DoStart()
    {
        _currentBeforeAttackCooldown = _maxBeforeAttackCooldown;
    }
    public override void DoUpdate()
    {
        _currentBeforeAttackCooldown -= Time.deltaTime;
        if (_currentBeforeAttackCooldown < 0.1f)
        {
            Attack();
            _owner.meleeAttackCooldown = _maxAfterAttackCooldown;
            _owner.ChangeState(nextState);
        }
    }
    public void Attack()
    {
        var locationToPlaceAttack = _owner.targetedPlayer.position;
        var targetRotation = Quaternion.LookRotation(Vector3.forward, _owner.targetedPlayer.position);
        Instantiate(_attackPrefab, locationToPlaceAttack, targetRotation);
    }
}
