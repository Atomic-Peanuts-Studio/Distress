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
}
