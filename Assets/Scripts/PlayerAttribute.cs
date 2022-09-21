using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    [Header("Health")]
    public float health;
    public float maxHealth;
    public float invincibiltyTime;
    public float invincibleTime;
    [Header("Combat")]
    public float heavyAttackDamage = 50.0f;
    public List<float> basicAttackDamage = new List<float>() {25.0f, 25.0f, 30.0f};
    public List<float> comboDelay = new List<float>() { 2.8f, 3.0f, 0f};
    public List<float> duration = new List<float>() { 0.5f, 0.2f, 1f};
}
