using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Player inventory is extendable
    //Currently player inventory has only weapon slots: one melee and one ranged weapon
    [SerializeField]
    public GameObject rangedWeapon;
    public GameObject meleeWeapon;
    public GameObject weaponToDrop;

    GameObject player;

    public void Start()
    {
        player = gameObject;
        //Create initial melee weapon using the default weapon in the player's hands (Attack GameObject, PlayerAttributes property on Player)
        meleeWeapon = GameObject.Find("DefaultMelee");
        SetDefaultMeleeAttributes(meleeWeapon);
        meleeWeapon.SetActive(false);
    }

    public void SwitchWeapon(GameObject newWeapon, bool isRangedWeapon)
    {
        if (isRangedWeapon)
        {
            weaponToDrop = rangedWeapon;
            rangedWeapon = newWeapon;
            SetNewAttributesOnWeapon(newWeapon);
            DropWeapon(weaponToDrop);
        }
        else
        {
            weaponToDrop = meleeWeapon;
            meleeWeapon = newWeapon;
            SetNewAttributesOnWeapon(newWeapon);
            DropWeapon(weaponToDrop);
        }
    }

    public void DropWeapon(GameObject weapon)
    {
        //Drop the previous weapon from the player's hand to the floor (only if the player has had previous weapons)
        if (weapon != null)
        {
            weapon.transform.position = player.transform.position;
            weapon.GetComponent<Collider2D>().enabled = true;
            weapon.SetActive(true);
        }
    }

    public void SetNewAttributesOnWeapon(GameObject weapon)
    {
        //Setting attributes, sprites, animations
        player.GetComponentsInChildren<SpriteRenderer>()[1].sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        player.GetComponent<PlayerAttribute>().AttackInfoArray = weapon.GetComponent<WeaponInformation>().attackInfo;
        StateMachine.Instance.weaponName = weapon.GetComponent<WeaponInformation>().animationName;
    }

    public void SetDefaultMeleeAttributes(GameObject weapon)
    {
        //Setting attributes, sprites, animations
        weapon.GetComponent<WeaponInformation>().sprite = player.GetComponentsInChildren<SpriteRenderer>()[1].sprite;
        weapon.GetComponent<WeaponInformation>().attackInfo = player.GetComponent<PlayerAttribute>().AttackInfoArray;
        weapon.GetComponent<WeaponInformation>().animationName = StateMachine.Instance.weaponName;
    }
}
