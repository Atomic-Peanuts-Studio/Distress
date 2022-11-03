using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> Enemies { get; private set; }
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
        MenuScore.LastScore += (int)(enemy.baseDifficulty * _scaling.ScalingPerTrigger + 1);
        if (Enemies.Contains(enemy)) Enemies.Remove(enemy);
    }
}
