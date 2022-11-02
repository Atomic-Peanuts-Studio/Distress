using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemiesInRangeState : EnemyStateWithWaiting
{
    [SerializeField] private float _healRange = 10f;
    [SerializeField] private float _healAmount = 10f;
    [SerializeField] private float _scareRange = 5f;
    public override void DoUpdate()
    {
        base.DoUpdate();
        if (Vector2.Distance(_owner.targetedPlayer.position, _owner.enemyRoot.position) < _scareRange) _owner.ChangeState(nextState);
    }
    internal override void PerformMainBehavior()
    {
        foreach (Enemy enemy in _owner.enemyManager.Enemies)
        {
            if (Vector2.Distance(enemy.enemyRoot.position, _owner.enemyRoot.position) < _healRange*_healRange)
            {
                enemy.GetComponent<Health>().Heal(_healAmount);
            }
        }
        OnMainBehaviorDone();
    }
}
