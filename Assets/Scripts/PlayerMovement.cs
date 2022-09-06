using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dash")]
    public float nextTeleport = 0.15f;
    public float cooldown = 1f;
    public float dashDistance = 0f;
    public float dashMaxDistance = 5f;
    public float dashCharge = 5f;
    public GameObject spriteRenderer;
    private GameObject Clone;
    private bool charged = false;
    private Vector3 destination;


    [Header("Movement")]
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Clone = GameObject.Instantiate(spriteRenderer, transform.position, Quaternion.identity);
        Clone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var worldPosition = Input.mousePosition;
        worldPosition.z = 10f;
        var facing = Camera.main.ScreenToWorldPoint(worldPosition) - transform.position;
        facing.z = 0f;
        destination = transform.position + facing.normalized * dashDistance;
        Clone.transform.position = destination;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
       if (Input.GetKey(KeyCode.LeftShift) && nextTeleport < Time.time)
        {
            ChargeTeleport();
        }
       else if (Input.GetKeyUp(KeyCode.LeftShift))
            charged = true;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = GetMouseWorldPosition();
            Vector3 attackDir = mousePos - transform.position;
           
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
        if (charged == true)
        {
            Teleport();
        }

    }


    private void Teleport()
    {
        Clone.SetActive(false);
        dashDistance = 0;
        charged = false;
        transform.position = destination;
        nextTeleport = Time.time + cooldown;
    }

    private void ChargeTeleport()
    {
        Clone.SetActive(true);
        Clone.transform.position = destination;
        if (dashDistance <= dashMaxDistance)
        {
            dashDistance += dashCharge * Time.deltaTime;
            if (dashDistance > dashMaxDistance)
            {
                dashDistance = dashMaxDistance;
                charged = true;
            }
            if (dashDistance == dashMaxDistance)
            {
                charged = true;
            }
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }
}
