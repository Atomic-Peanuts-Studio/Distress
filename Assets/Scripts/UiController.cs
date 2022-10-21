using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject button;
    public Health health;
    public TextMeshProUGUI hpText;

    public void Start()
    {
        health.deathEvent.AddListener(KillPlayer);
    }
    public void UpdateHealth(float newHealth)
    {
        hpText.text = newHealth + "/" + health.maxHealth;
    }
    public void KillPlayer()
    {
        button.SetActive(true);
    }
}
