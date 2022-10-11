using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinionsState : EnemyStateWithWaiting
{
    [SerializeField] private float _amountOfEnemies = 3f;
    [SerializeField] private float _maxSpawnRange = 5f;
    [SerializeField] private GameObject _enemyPrefab;
    private void OnEnable()
    {
        Type = StateType.SpawnMinions;
    }
    internal override void PerformMainBehavior()
    {
        for (int i = 0; i < _amountOfEnemies; i++)
        {
            var randomLocation = (Vector2)_owner.enemyRoot.position + Random.insideUnitCircle * Random.Range(0, _maxSpawnRange);
            Instantiate(_enemyPrefab, randomLocation, Quaternion.identity);
        }
        OnMainBehaviorDone();
    }
}
