using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health _healthToTrack;
    [SerializeField] private Slider _slider;
    private void Start()
    {
        _slider.maxValue = _healthToTrack.maxHealth;
        _slider.value = _healthToTrack.health;
        _healthToTrack.healthChanged += UpdateHealthBar;
    }
    private void UpdateHealthBar(float newHealth)
    {
        _slider.value = _healthToTrack.health;
    }
}
