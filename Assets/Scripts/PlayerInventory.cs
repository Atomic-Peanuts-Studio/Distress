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
        //Set meleeWeapon to the default weapon in the player's hands
        meleeWeapon = new GameObject();
        meleeWeapon.name = "meleeWeapon";
        meleeWeapon.AddComponent<SpriteRenderer>();
        meleeWeapon.AddComponent<BoxCollider2D>();
        meleeWeapon.GetComponent<BoxCollider2D>().size = new Vector2(0.16f, 0.16f);
        meleeWeapon.transform.localScale = new Vector3(6f, 6f, 0f);
        meleeWeapon.GetComponent<BoxCollider2D>().isTrigger = true;
        meleeWeapon.AddComponent<Pickup>();
        meleeWeapon.AddComponent<WeaponInformation>();
        meleeWeapon.GetComponent<Collider2D>().enabled = false;
        meleeWeapon.SetActive(false);
        SetDefaultAttributes(meleeWeapon);
    }

    public void SwitchWeapon(GameObject newWeapon, bool isRangedWeapon)
    {
        if (isRangedWeapon)
        {
            weaponToDrop = rangedWeapon;
            rangedWeapon = newWeapon;
            SetAttributesOnWeapon(newWeapon);
            DropPreviousWeapon(weaponToDrop);
        }

        else
        {
            weaponToDrop = meleeWeapon;
            meleeWeapon = newWeapon;
            SetAttributesOnWeapon(newWeapon);
            DropPreviousWeapon(weaponToDrop);
        }
    }

    public void DropPreviousWeapon(GameObject weaponToDrop)
    {
        //Drop the previous weapon from the player's hand to the floor (only if the player has had previous weapons)
        if (weaponToDrop != null)
        {
            weaponToDrop.transform.position = player.transform.position;
            weaponToDrop.transform.parent = null;
            weaponToDrop.SetActive(true);
            weaponToDrop.GetComponent<Collider2D>().enabled = true;
        }
    }

    public void SetAttributesOnWeapon(GameObject weapon)
    {
        //Setting attributes, sprites, animations
        player.GetComponentsInChildren<SpriteRenderer>()[1].sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        player.GetComponent<PlayerAttribute>().AttackInfoArray = weapon.GetComponent<WeaponInformation>().attackInfo;
        StateMachine.Instance.weaponName = weapon.GetComponent<WeaponInformation>().animationName;
    }

    public void SetDefaultAttributes(GameObject weapon)
    {
        //Setting attributes, sprites, animations
        weapon.GetComponent<WeaponInformation>().sprite = player.GetComponentsInChildren<SpriteRenderer>()[1].sprite;
        weapon.GetComponent<WeaponInformation>().attackInfo = player.GetComponent<PlayerAttribute>().AttackInfoArray;
        weapon.GetComponent<WeaponInformation>().animationName = StateMachine.Instance.weaponName;
    }
}
