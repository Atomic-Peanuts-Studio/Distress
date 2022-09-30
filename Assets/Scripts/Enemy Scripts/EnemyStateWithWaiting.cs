using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateWithWaiting : EnemyState
{
    [SerializeField] internal float maxInTime = 2f;
    [SerializeField] internal float maxOutTime = 2f;
    internal float inTimer;
    internal float outTimer;
    internal bool hasExecuted = false;
    public override void DoStart()
    {
        inTimer = maxInTime;
        hasExecuted = false;
    }
    public override void DoUpdate()
    {
        if (inTimer > 0.1f) inTimer -= Time.deltaTime;
        else if (!hasExecuted) PerformMainBehavior();
        if (outTimer > 0.1f) outTimer -= Time.deltaTime;
        else if (hasExecuted) _owner.ChangeState(nextState);
    }
    internal void OnMainBehaviorDone()
    {
        hasExecuted = true;
        outTimer = maxOutTime;
    }
    internal virtual void PerformMainBehavior() { }
}
