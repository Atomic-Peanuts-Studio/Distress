using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> Enemies { get; private set; }
    [SerializeField] private PlayerScoreManager _scoreManager;
    [SerializeField] private EnemyScaling _scaling;
    private void Awake()
    {
        Enemies = new List<Enemy>();
    }
    public void AddEnemyToList(Enemy enemy)
    {
        Enemies.Add(enemy);
    }
    public void EnemyDied(Enemy enemy)
    {
        _scoreManager.AddScore(enemy.baseDifficulty * _scaling.ScalingPerTrigger);
        if (Enemies.Contains(enemy)) Enemies.Remove(enemy);
    }
}
