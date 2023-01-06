using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerPerk : Perk
{
    public override string Name => "Ranger";
    public override string Description => "Shoot more, be more vulnerable";
    public override void PerformBehavior (PlayerAttribute attributes)
    {
        attributes.maxHealth -= 10;
        attributes.maxMana += 2;
    }
}
