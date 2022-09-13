using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyAIState
{
    Idle,
    Chasing,
    Attacking,
    Repositioning
}
public class EnemyAI : MonoBehaviour
{
    public EnemyAIState State { get; private set; }
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _repositioningDistance = 2f;
    [SerializeField] private Transform _player;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private GameObject _attackPrefab;
    [SerializeField] private Transform _enemyRoot;
    [SerializeField] private EnemyMovement _movement;
    private float _currentAttackCooldown = 0f;
    private void OnEnable()
    {
        State = EnemyAIState.Chasing;
    }
    private void Update()
    {
        switch (State)
        {
            case EnemyAIState.Chasing:
                ChasingUpdate();
                break;
            case EnemyAIState.Repositioning:
                RepositioningUpdate();
                break;
            case EnemyAIState.Attacking:
                AttackingUpdate();
                break;
        }
    }
    private void ChangeState(EnemyAIState newState)
    {
        State = newState;
    }
    private void AttackingUpdate()
    {
        Attack();
        _currentAttackCooldown = _attackCooldown;
        ChangeState(EnemyAIState.Repositioning);
    }
    private void ChasingUpdate()
    {
        if (_player && Vector2.Distance(_player.position, _enemyRoot.position) > _attackRange)
        {
            _movement.MoveTowards(_player.position);
            _enemyRoot.rotation = Quaternion.LookRotation(Vector3.forward, _player.position);
        }
        else ChangeState(EnemyAIState.Attacking);
    }
    private void RepositioningUpdate()
    {
        if (_currentAttackCooldown > 0.1f)
        {
            _currentAttackCooldown -= Time.deltaTime;
            if (Vector2.Distance(_player.position, _enemyRoot.position) < _repositioningDistance)
            {
                var positionAwayFromPlayer = _enemyRoot.position - _player.position;
                positionAwayFromPlayer.Normalize();
                positionAwayFromPlayer *= _repositioningDistance;
                _movement.MoveTowards(positionAwayFromPlayer);
                _enemyRoot.rotation = Quaternion.LookRotation(Vector3.forward, _player.position);
            }
        }
        else ChangeState(EnemyAIState.Chasing);
        
    }
    private void Attack()
    {
        var locationToPlaceAttack = _player.position;
        var targetRotation = Quaternion.LookRotation(Vector3.forward, _player.position);
        Instantiate(_attackPrefab, locationToPlaceAttack, targetRotation);
    }
}
