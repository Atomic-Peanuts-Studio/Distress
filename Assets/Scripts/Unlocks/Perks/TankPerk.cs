using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPerk : Perk
{
    public override string Name => "Tank";
    public override string Description => "Have more health, move slower";
    public override void PerformBehavior(PlayerAttribute attributes)
    {
        attributes.moveSpeed -= 2;
        attributes.maxHealth += 10;
    }
}
