using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguePerk : Perk
{
    public override string Name => "Rogue";
    public override string Description => "Move quicker, be able to take less hits";
    public override void PerformBehavior(PlayerAttribute attributes)
    {
        attributes.health -= 10;
        attributes.moveSpeed += 2;
    }
}
