using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyState ActiveState { get; private set; }
    [SerializeField] private EnemyState[] possibleStates;
    public Transform enemyRoot;
    public Transform targetedPlayer;
    public EnemyMovement movement;
    public float currentAttackCooldown;
    private void OnEnable()
    {
        ActiveState = FindStateByType(StateType.Idle);
    }
    private void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
        ActiveState.DoUpdate();
    }
    public void ChangeState(StateType preferredNewState)
    {
        var newState = FindStateByType(preferredNewState);
        newState.DoStart();
        ActiveState = newState;
    }
    private EnemyState FindStateByType(StateType type)
    {
        List<EnemyState> foundStates = new List<EnemyState>();
        foreach (EnemyState state in possibleStates)
        {
            if (state.Type == type) foundStates.Add(state);
        }
        if (foundStates.Count == 1) return foundStates[0];
        else if (foundStates.Count == 0) throw new System.Exception("No state of this type!");
        else return foundStates[0];
    }
}
