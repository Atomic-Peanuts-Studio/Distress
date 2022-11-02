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
    [HideInInspector] public float rangedAttackCooldown;
    [SerializeField] private EnemyState _firstState;
    public EnemyManager enemyManager;
    public Animator animator;
    private void Start()
    {
        ActiveState = _firstState;
        targetedPlayer = FindObjectOfType<PlayerAttribute>().transform;
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.AddEnemyToList(this);
    }
    private void Update()
    {
        if(meleeAttackCooldown > 0.1f) meleeAttackCooldown -= Time.deltaTime;
        if(rangedAttackCooldown > 0.1f) rangedAttackCooldown -= Time.deltaTime;
        ActiveState.DoUpdate();
    }
    public void ChangeState(EnemyState preferredNewState)
    {
        preferredNewState.DoStart();
        ActiveState = preferredNewState;
    }
    private void OnDestroy()
    {
        enemyManager.RemoveEnemyFromList(this);
    }
}
