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
    private bool charging = false;
    private Vector3 destination;
    public float graceTime = 1f;
    public float elapsedTime = 0f;
    public float timeNow = 0f;
    private bool tookTime = false;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    Vector2 movement;
    static float channelingTime = 0.0f;
    public float minimum = 0f;
    public float maximum = 10f;

    [Header("Health")]
    private Health healthScript;
    private bool dead;

    [Header("UI")]
    public UiController uiController;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = this.gameObject.GetComponent<Health>();
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Clone = GameObject.Instantiate(spriteRenderer, transform.position, Quaternion.identity);
        Clone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript.dead)
        {
            uiController.KillPlayer();
            return;
        }
        var worldPosition = Input.mousePosition;
        worldPosition.z = 10f;
        var facing = Camera.main.ScreenToWorldPoint(worldPosition) - transform.position;
        facing.z = 0f;
        destination = transform.position + facing.normalized * dashDistance;
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
            // Ease in and out slower walking during channeling/charging the teleport
            rb.MovePosition(rb.position + movement * Mathf.Lerp(maximum, minimum, channelingTime) * Time.fixedDeltaTime);
            channelingTime += 0.5f * Time.fixedDeltaTime;
            if (channelingTime > 1.0f)
            {
                float temp = minimum;
                minimum = maximum;
                maximum = temp;
                channelingTime = 0.0f;
            }
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void Teleport()
    {
        Clone.SetActive(false);
        dashDistance = 0;
        charged = false;
        charging = false;
        tookTime = false;
        transform.position = destination;
        nextTeleport = Time.time + cooldown;
    }

    private void ChargeTeleport()
    {
        if (!charging)
        {
            Clone.transform.position = transform.position;
        }
        charging = true;
        Clone.SetActive(true);
        if (charging == true)
        {
            Clone.transform.position = Vector3.MoveTowards(Clone.transform.position, destination, moveSpeed * Time.deltaTime);
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

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }
}
