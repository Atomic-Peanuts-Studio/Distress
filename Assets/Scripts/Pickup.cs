using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Input")]
    public PlayerMovement input;

    PlayerInventory inventory;
    GameObject weaponToDrop;
    GameObject player;
    bool performingPickupAction=false;

    //Setting attributes, sprites, animations
    WeaponInformation weaponInformation;
    SpriteRenderer spriteRenderer;
    SpriteRenderer attackSprite;
    PlayerAttribute playerAttribute;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        input = player.GetComponent<PlayerMovement>();
        inventory = player.GetComponent<PlayerInventory>();

        //Setting attributes, sprites, animations
        spriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        attackSprite = player.GetComponentsInChildren<SpriteRenderer>()[1];
        weaponInformation = gameObject.GetComponent<WeaponInformation>();
        playerAttribute=player.GetComponent<PlayerAttribute>();
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
                if (!weaponInformation.isRangedWeaponType)
                {
                    weaponToDrop = inventory.meleeWeapon;
                    inventory.meleeWeapon = gameObject;

                    //Setting attributes, sprites, animations
                    attackSprite.sprite = spriteRenderer.sprite;
                    playerAttribute.AttackInfoArray = weaponInformation.attackInfo;
                    StateMachine.Instance.weaponName = weaponInformation.animationName;
                }

                else
                {
                    weaponToDrop = inventory.rangedWeapon;
                    inventory.rangedWeapon = gameObject;

                    //Setting attributes, sprites, animations
                    attackSprite.sprite = spriteRenderer.sprite;
                    playerAttribute.AttackInfoArray = weaponInformation.attackInfo;
                    StateMachine.Instance.weaponName = weaponInformation.animationName;
                }
                //Move the new weapon from the floor to the player's hand
                gameObject.transform.parent = player.transform;
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.SetActive(false);

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
