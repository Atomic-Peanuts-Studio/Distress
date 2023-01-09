using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPerk : Perk
{
    public override string Name => "Normal";
    public override string Description => "The standard playstyle";
    public override bool IsAvailable => true;
    public override Color Color => Color.white;
    public override void PerformBehavior(PlayerAttribute attributes)
    {
    }
}
