using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    public static event Action<PerkButton> OnSelected;
    public Perk RelatedPerk { get { return _relatedPerk; } }

    [Header("Functional")]
    [SerializeField] Perk _relatedPerk; 
    [SerializeField] bool _isSelected = false;

    [Header("Visual")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _root;
    [SerializeField] private float _colorGrayingAmount = 0.5f;

    private Color _grayifiedColor;

    private void Start()
    {
        if (_relatedPerk == null) _relatedPerk = GetComponent<Perk>();
        _grayifiedColor = new Color(_relatedPerk.Color.r - _colorGrayingAmount, _relatedPerk.Color.g - _colorGrayingAmount, _relatedPerk.Color.b - _colorGrayingAmount);
        UpdateColor();
        InitializeButtonColors();
        _text.SetText(_relatedPerk.Name);
        if (_relatedPerk.GetType() == typeof(NormalPerk)) ChangeSelected(true);
    }

    public void ChangeSelected(bool newSelected)
    {
        if (newSelected) OnSelected?.Invoke(this);
        _isSelected = newSelected;
        UpdateColor();
    }

    public void UpdateColor()
    {
        if (_text) _text.color = _isSelected ? _relatedPerk.Color : _grayifiedColor;
        else throw new MissingComponentException("No text field referenced!");
    }

    private void InitializeButtonColors()
    {
        ColorBlock newColors = new ColorBlock();
        newColors.normalColor = _grayifiedColor;
        newColors.highlightedColor = _relatedPerk.Color;
        newColors.pressedColor = _grayifiedColor;
        newColors.selectedColor = _relatedPerk.Color;
        newColors.fadeDuration = 0.1f;
        newColors.colorMultiplier = 1f;
        _button.colors = newColors;
    }
}
