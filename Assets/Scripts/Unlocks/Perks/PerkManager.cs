using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public ICollection<Perk> AvailablePerks { get { return _possiblePerks.FindAll(p => p.IsAvailable).AsReadOnly(); } }
    public ICollection<Perk> PossiblePerks { get { return _possiblePerks.AsReadOnly(); } }

    [SerializeField] private List<Perk> _possiblePerks;

    public void SelectPerk(Perk perkToSelect)
    {
        PlayerPrefs.SetString("CurrentPerk", perkToSelect.Name);
    }

    public Perk GetCurrentPerk()
    {
        Perk selectedPerk = _possiblePerks.Find(p => p.Name == PlayerPrefs.GetString("CurrentPerk"));
        if (selectedPerk == null) throw new System.Exception("No current perk found!");
        return selectedPerk;
    }

    public void UnlockPerk(Perk perkToUnlock)
    {
        perkToUnlock.Unlock();
    }
}
