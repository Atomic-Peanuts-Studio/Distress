using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float nextTeleport = 0.15f;
    public float cooldown = 1f;
    public float dashDistance = 0f;
    public float dashMaxDistance = 5f;
    public float dashCharge = 5f;
    public GameObject spriteRenderer;
    private GameObject Clone;
    private bool charged = false;
    private Vector3 destination;
    public TextMeshProUGUI text;
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
       
        text.text = dashDistance.ToString();
        if (charged == true || (Input.GetKeyUp(KeyCode.LeftShift) && dashCharge > 0))
        {
           
        }
        if (Input.GetKey(KeyCode.LeftShift) && nextTeleport < Time.time)
        {

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            charged = true;


    }

    private void FixedUpdate()
    {
     
    }
}
