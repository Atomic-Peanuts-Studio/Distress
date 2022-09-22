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
    public float _attackCooldown = 2f;
    public float _attackCharge = 0.5f;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _repositioningDistance = 2f;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _attackPrefab;
    [SerializeField] private Transform _enemyRoot;
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private float _spotRange = 2.5f;
    [SerializeField] private float _idleRepositionTime = 2f;
    private float _currentAttackCooldown = 0f;
    private float _currentAttackChargeTimer = 0f;
    private float _currentIdleRepositionTimer = 0f;
    private void OnEnable()
    {
        State = EnemyAIState.Idle;
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
            case EnemyAIState.Idle:
                IdleUpdate();
                break;
        }
    }
    private void ChangeState(EnemyAIState newState)
    {
        if (newState == EnemyAIState.Attacking)
        {
            _currentAttackChargeTimer = _attackCharge;
        }
        State = newState;
    }
    private void AttackingUpdate()
    {
        _currentAttackChargeTimer -= Time.deltaTime;
        if (_currentAttackChargeTimer < 0.1f)
        {
            Attack();
            _currentAttackCooldown = _attackCooldown;
            ChangeState(EnemyAIState.Repositioning);
        }
    }
    private void ChasingUpdate()
    {
        if (_player && Vector2.Distance(_player.position, _enemyRoot.position) > _attackRange)
        {
            _movement.MoveTowards(_player.position);
            //_enemyRoot.rotation = Quaternion.LookRotation(Vector3.forward, _player.position);
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
               // _enemyRoot.rotation = Quaternion.LookRotation(Vector3.forward, _player.position);
            }
        }
        else ChangeState(EnemyAIState.Chasing);
        
    }
    private void IdleUpdate()
    {
        _currentIdleRepositionTimer -= Time.deltaTime;
        if (_currentIdleRepositionTimer < 0.1f) _movement.MoveToRandomLocation(5f);
        if (Vector2.Distance(_player.position, _enemyRoot.position) < _spotRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(_enemyRoot.position, _player.position - _enemyRoot.position);
            if (hit.collider != null && hit.collider.CompareTag("Player")) ChangeState(EnemyAIState.Chasing);
        }
    }
    private void Attack()
    {
        var locationToPlaceAttack = _player.position;
        var targetRotation = Quaternion.LookRotation(Vector3.forward, _player.position);
        Instantiate(_attackPrefab, locationToPlaceAttack, targetRotation);
    }
}
