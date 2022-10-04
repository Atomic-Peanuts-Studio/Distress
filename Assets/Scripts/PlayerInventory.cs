using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Player inventory is extendable
    //Currently player inventory has only weapon slots: one melee and one ranged weapon
    [SerializeField]
    public GameObject rangedWeapon;
    public GameObject meleeWeapon;
}
