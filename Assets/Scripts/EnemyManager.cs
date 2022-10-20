using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> Enemies { get; private set; }
    private void Awake()
    {
        Enemies = new List<Enemy>();
    }
    public void AddEnemyToList(Enemy enemy)
    {
        Enemies.Add(enemy);
    }
    public void RemoveEnemyFromList(Enemy enemy)
    {
        if (Enemies.Contains(enemy)) Enemies.Remove(enemy);
    }
}
