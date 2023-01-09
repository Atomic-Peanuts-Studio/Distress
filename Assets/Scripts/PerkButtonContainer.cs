using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkButtonContainer : MonoBehaviour
{
    public PerkButton CurrentSelectedPerkButton { get; private set; }

    [SerializeField] private List<PerkButton> _allPerkButtons = new List<PerkButton>();
    [SerializeField] private PerkManager _perkManager;
    [SerializeField] private GameObject _perkButtonPrefab;

    private void Start()
    {
        PerkButton.OnSelected += ChangeSelectedButton;
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        foreach(Perk perk in _perkManager.AvailablePerks)
        {
            GameObject newPerkButton = Instantiate(_perkButtonPrefab, transform);
            newPerkButton.AddComponent(perk.GetType());
            _allPerkButtons.Add(newPerkButton.GetComponent<PerkButton>());
        }
    }

    private void ChangeSelectedButton(PerkButton newSelected)
    {
        CurrentSelectedPerkButton = newSelected;

        foreach (PerkButton button in _allPerkButtons)
        {
            if (button != newSelected) button.ChangeSelected(false);
        }
    }

    public void SetSelectedPerk()
    {
        _perkManager.SelectPerk(CurrentSelectedPerkButton.RelatedPerk);
    }
}
