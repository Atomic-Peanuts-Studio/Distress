using UnityEngine;

public class Pickup : MonoBehaviour
{
    PlayerInventory inventory;
    GameObject weaponToDrop;
    GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<PlayerInventory>();
            if (gameObject.tag == "MeleeWeapon")
            {
                weaponToDrop = inventory.meleeWeapon;
                inventory.meleeWeapon = gameObject;
            }

            else
            {
                weaponToDrop = inventory.rangedWeapon;
                inventory.rangedWeapon = gameObject;
            }
            //Move the new weapon from the floor to the player's hand
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.parent = player.transform;

            // Drop the previous weapon from the player's hand to the floor (only if the player has had previous weapons)
            if (weaponToDrop != null)
            {
                weaponToDrop.transform.position = player.transform.position;
                weaponToDrop.transform.parent = null;

                //TODO: Implement enabling collider again when the player leaves the weapon's location (to prevent bugs)
                //weaponToDrop.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
