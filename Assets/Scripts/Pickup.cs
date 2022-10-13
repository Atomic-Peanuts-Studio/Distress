using UnityEngine;

public class Pickup : MonoBehaviour
{
    GameObject player;
    bool performingPickupAction=false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player.GetComponent<PlayerMovement>().controls.Player.Interact.IsPressed())
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
                player.GetComponent<PlayerInventory>().SwitchWeapon(gameObject, gameObject.GetComponent<WeaponInformation>().isRangedWeaponType);
                gameObject.SetActive(false);
            }
            performingPickupAction = false;
        }
    }
}
