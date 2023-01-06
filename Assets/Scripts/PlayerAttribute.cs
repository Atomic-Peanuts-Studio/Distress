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
    public float invincibleTime;
    [Header("Mana")]
    public float mana = 4;
    public float maxMana = 4;
    public float manaRestoreOnKill = 1;
    public event Action manaChanged;
    [Header("Movement")]
    public float moveSpeed = 5f;
    [Header("Singleton")]
    public static PlayerAttribute instance;
    public void Awake()
    {
        instance = this;
    }
    public void addMana()
    {
        if(mana + manaRestoreOnKill < maxMana)
        {
            mana += manaRestoreOnKill;
        }
        else
        {
            mana = maxMana;
        }
        manaChanged?.Invoke();
    }
    public void removeMana(float manaToUse)
    {
        mana -= manaToUse;
        manaChanged?.Invoke();
    }
    [Header("Combat")]
    [SerializeField] public AttackInfo[] AttackInfoArray;
}
