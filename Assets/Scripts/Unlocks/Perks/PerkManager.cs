using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public Perk[] PossiblePerks { get { return _possiblePerks; } set { _possiblePerks = value; } }
    public Perk ActivePerk { get; private set; }

    [SerializeField] private Perk[] _possiblePerks;
    [SerializeField] private PlayerAttribute _attributes;

    public void SelectPerk(Perk perkToSelect)
    {
        ActivePerk = perkToSelect;
        perkToSelect.PerformBehavior(_attributes);
    }

    public void UnlockPerk(Perk perkToUnlock)
    {
        perkToUnlock.Unlock();
    }
}
