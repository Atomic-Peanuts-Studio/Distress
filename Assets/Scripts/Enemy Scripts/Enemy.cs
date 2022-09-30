using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyState ActiveState { get; private set; }
    [SerializeField] private EnemyState[] possibleStates;
    public Transform enemyRoot;
    public Transform targetedPlayer;
    public EnemyMovement movement;
    [HideInInspector] public float meleeAttackCooldown;
    private float wtfTimer = 0f;
    public Animator animator;
    private void OnEnable()
    {
        ActiveState = FindStateByType(StateType.Idle);
        targetedPlayer = FindObjectOfType<PlayerAttribute>().transform;
    }
    private void Update()
    {
        if(meleeAttackCooldown > 0.1f) meleeAttackCooldown -= Time.deltaTime;
        ActiveState.DoUpdate();
    }
    public void ChangeState(EnemyState preferredNewState)
    {
        wtfTimer++;
        Debug.Log(preferredNewState.ToString() + " " + wtfTimer);
        if (!possibleStates.Contains(preferredNewState)) throw new System.Exception("State " + preferredNewState.ToString() + " not found in possible states!");
        preferredNewState.DoStart();
        ActiveState = preferredNewState;
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
