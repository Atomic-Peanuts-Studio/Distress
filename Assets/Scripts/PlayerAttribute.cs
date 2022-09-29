using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    [Serializable]
    public class AttackInfo
    {
        public float attackDamage;
        public float attackDuration;
        public float comboDelay;
    }

    [Header("Health")]
    public float health;
    public float maxHealth;
    public float invincibiltyTime;
    public float invincibleTime;
    [Header("Mana")]
    public float mana = 100;
    public void addMana()
    {
        if(mana + 25 < 100)
        {
            mana += 25;
        }
        else
        {
            mana = 100;
        }
    }
    [Header("Combat")]
    [SerializeField] public AttackInfo[] AttackInfoArray;
}
