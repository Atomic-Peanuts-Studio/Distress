using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StateType
{
    Idle,
    MeleeAttack,
    RangedAttack,
    Chase,
    Reposition,
    SpawnMinions,
    SpawnTraps
}
public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] internal Enemy _owner;
    public StateType Type { get; internal set; }
    public EnemyState nextState;
    public virtual void DoUpdate() { }
    public virtual void DoStart() { }
    public override string ToString()
    {
        return this.GetType().ToString();
    }
}
