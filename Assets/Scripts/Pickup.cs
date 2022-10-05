using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Input")]
    public PlayerMovement input;

    PlayerInventory inventory;
    GameObject weaponToDrop;
    GameObject player;
    bool performingPickupAction=false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        input = player.GetComponent<PlayerMovement>();
        inventory = player.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (input.controls.Player.Interact.IsPressed())
        {
            performingPickupAction = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (performingPickupAction)
            {
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
                gameObject.transform.parent = player.transform;
                gameObject.GetComponent<Collider2D>().enabled = false;

                // Drop the previous weapon from the player's hand to the floor (only if the player has had previous weapons)
                if (weaponToDrop != null)
                {
                    weaponToDrop.transform.parent = null;
                    weaponToDrop.GetComponent<Collider2D>().enabled = true;
                }
                performingPickupAction = false;
            }
        }
    }
}
