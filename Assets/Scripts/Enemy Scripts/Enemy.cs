using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyState ActiveState { get; private set; }
    public Transform enemyRoot;
    public Transform targetedPlayer;
    public EnemyMovement movement;
    [HideInInspector] public float meleeAttackCooldown;
    [SerializeField] private EnemyState _firstState;
    public Animator animator;
    private void OnEnable()
    {
        ActiveState = _firstState;
        targetedPlayer = FindObjectOfType<PlayerAttribute>().transform;
    }
    private void Update()
    {
        if(meleeAttackCooldown > 0.1f) meleeAttackCooldown -= Time.deltaTime;
        ActiveState.DoUpdate();
    }
    public void ChangeState(EnemyState preferredNewState)
    {
        preferredNewState.DoStart();
        ActiveState = preferredNewState;
    }
}
