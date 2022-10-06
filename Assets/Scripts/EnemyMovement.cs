using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyMovementState
{
    Idle,
    MovingTowards
}
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _enemyRoot;
    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    public void MoveTowards(Vector2 position)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 100f, -1);
        _agent.destination = hit.position;
    }
    public void MoveToRandomLocation(float range)
    {
        var direction = Random.insideUnitCircle * range;
        var position = direction + (Vector2)_enemyRoot.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 100f, -1);
        _agent.destination = hit.position;
    }
    public void StandStill()
    {
        _agent.isStopped = true;
    }
}
