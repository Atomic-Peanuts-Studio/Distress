using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyMovementState
{
    Idle,
    MovingTowards
}
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private Transform _enemyRoot;
    private Vector2 _positionToMoveTowards;
    public EnemyMovementState State { get; private set; }
    private void OnEnable()
    {
        State = EnemyMovementState.Idle;
    }
    private void Update()
    {
        if (State == EnemyMovementState.MovingTowards)
        {
            var newPosition = Vector2.MoveTowards(_enemyRoot.position, _positionToMoveTowards, _movementSpeed * Time.deltaTime);
            _enemyRoot.position = newPosition;
        }
    }
    public void MoveTowards (Vector2 position)
    {
        if (position != null)
        {
            _positionToMoveTowards = position;
            State = EnemyMovementState.MovingTowards;
        }

    }
    public void StopMoving ()
    {
        State = EnemyMovementState.Idle;
    }
}
