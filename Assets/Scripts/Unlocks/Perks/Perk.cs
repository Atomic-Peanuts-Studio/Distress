using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk : MonoBehaviour
{
    public virtual string Name { get; private set; }
    public virtual string Description { get; private set; }
    public virtual Color Color { get; private set; }
    public virtual bool IsAvailable { get { return PlayerUnlock.IsUnlocked(Name); } }
    public virtual void PerformBehavior(PlayerAttribute attributes) { }
    public void Unlock()
    {
        PlayerUnlock.Unlock(Name);
    }
}
