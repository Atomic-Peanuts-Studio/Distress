using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrapsState : EnemyStateWithWaiting
{
    [SerializeField] private BossTrap[] _trapsUnderControl;
    private float _trapsDone;
    public override void DoStart()
    {
        base.DoStart();
        _trapsDone = 0f;
    }
    internal override void PerformMainBehavior()
    {
        foreach (BossTrap trap in _trapsUnderControl)
        {
            trap.triggerTrap();
            trap.OnTrapDone += OnSingleTrapDone;
        }
    }
    private void OnSingleTrapDone()
    {
        _trapsDone++;
        if (_trapsDone >= _trapsUnderControl.Length) OnMainBehaviorDone();
    }
}
