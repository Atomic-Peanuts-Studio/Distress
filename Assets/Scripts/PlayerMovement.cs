using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;

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
    private bool charging = false;
    private Vector3 destination;
    public float graceTime = 1f;
    public float elapsedTime = 0f;
    public float timeNow = 0f;
    private bool tookTime = false;
    Rigidbody2D cloneRB;

    [Header("Movement")]
    public Controls controls;
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    Vector2 movement;
    public float increment;

    [Header("Health")]
    private Health healthScript;
    private bool dead;
    
    [Header("UI")]
    public UiController uiController;

    // Start is called before the first frame update
    void Start()
    {
        controls=new Controls();
        controls.Player.Enable();
        healthScript = this.gameObject.GetComponent<Health>();
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Clone = GameObject.Instantiate(spriteRenderer, transform.position, Quaternion.identity);
        cloneRB = Clone.GetComponent<Rigidbody2D>();
        Clone.SetActive(false);
        increment = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript.dead)
        {
            uiController.KillPlayer();
            return;
        }
        Vector3 worldPosition = controls.Player.Point.ReadValue<Vector2>();
        worldPosition.z = 10f;
        var facing = Camera.main.ScreenToWorldPoint(worldPosition) - transform.position;
        facing.z = 0f;
        destination = transform.position + facing.normalized * dashDistance;
        movement = controls.Player.Move.ReadValue<Vector2>();
        if (controls.Player.Teleport.IsPressed() && nextTeleport < Time.time)
        {
            ChargeTeleport();
        }
        else if (controls.Player.Teleport.WasReleasedThisFrame() && charging)
        {
            charged = true;
        }
    }

    private void FixedUpdate()
    {
        if (healthScript.dead)
        {
            return;
        }
        if (charged == true)
        {
            Teleport();
        }
        if (charging == true)
        {
            // Ease in slower walking during channeling/charging the teleport
            increment += 0.5f * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement * moveSpeed / increment * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            // Create a small inertia to smoothen movement
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(new Vector2(0, moveSpeed));
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(new Vector2(0, -moveSpeed));
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(new Vector2(-moveSpeed, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(new Vector2(moveSpeed, 0));
            }
        }
    }

    private void Teleport()
    {
        Clone.SetActive(false);
        dashDistance = 0;
        charged = false;
        charging = false;
        increment = 1f;
        tookTime = false;
        transform.position = Clone.transform.position;
        nextTeleport = Time.time + cooldown;
    }

    private void ChargeTeleport()
    {
        if (!charging)
        {
            Clone.transform.position = transform.position;
            charging = true;
        }
        Clone.SetActive(true);
        if (charging == true)
        {
            cloneRB.MovePosition(Vector3.MoveTowards(Clone.transform.position, destination, moveSpeed * 10 * Time.deltaTime));
            if (dashDistance >= dashMaxDistance)
            {
                float distance = Vector3.Distance(Clone.transform.position, transform.position);
                // Restrict the clone distance at maximum distance within a circle radius from the player's position
                Vector3 fromOriginToObject = Clone.transform.position - transform.position;
                fromOriginToObject *= dashMaxDistance/1.2f / distance;
                cloneRB.MovePosition(Vector3.MoveTowards(transform.position + fromOriginToObject, destination, moveSpeed *10 * Time.deltaTime));
            }
        }
        if (dashDistance <= dashMaxDistance)
        {
            dashDistance += dashCharge * Time.deltaTime;
            if (dashDistance > dashMaxDistance)
            {
                dashDistance = dashMaxDistance;
                timeNow = Time.time;
                if (!tookTime)
                {
                    tookTime = true;
                    elapsedTime = timeNow + graceTime;
                }
                if (timeNow >= elapsedTime)
                {
                    charged = true;
                }
            }
            if (dashDistance == dashMaxDistance)
            {
                if (timeNow >= elapsedTime)
                {
                    charged = true;
                }
            }
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(controls.Player.Point.ReadValue<Vector2>());
        vec.z = 0f;
        return vec;
    }
}
