using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enemy Scaling:
 * 1. Have a box to simulate door to next level
 * 2. Have a public variable to see how much the box have been triggered
 * 3. For each number in the variable, increment health and damage by *2
 * Errors:
 * 
 */
public class EnemyScaling : MonoBehaviour
{
    [Header("Public Variables")]
    public bool DoorCollisionTrigger = false;
    public int ScalingPerTrigger = ScalingVariable.ScalingValue;
    [Header("Gameobjects")]
    public List<Health> EnemyHealth = new List<Health>();
    public ScytheSwipe EnemyDmg;
    private GameObject _Instance;

    // Start is called before the first frame update
    void Start()
    {
        EnemyDmg.GetComponent<ScytheSwipe>();
    }

    public void Awake()
    {
        // Check if gameobject with script exists
        if (!_Instance)
        {
            _Instance = this.gameObject;

            // Apply damage and health scaling immediately after starting game
            for (int i = 0; i < EnemyHealth.Count; i++)
            {
                EnemyHealth[i].health *= ScalingPerTrigger;
                EnemyHealth[i].maxHealth *= ScalingPerTrigger;
            }
            EnemyDmg._damage *= ScalingPerTrigger;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //Duplicate GameManager created every time the scene is loaded
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScalingVariable.ScalingValue = ScalingPerTrigger;    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Make sure to trigger if only the player collides with it
        if (collision.gameObject.CompareTag("Player")) 
        {
            DoorCollisionTrigger = true;
            ScalingPerTrigger += 1;

            // Scale the health and dmg (Switch to for each later)
            for (int i = 0; i < EnemyHealth.Count; i++)
            {
                EnemyHealth[i].health *= ScalingPerTrigger;
                EnemyHealth[i].maxHealth *= ScalingPerTrigger;
            }

            EnemyDmg._damage *= ScalingPerTrigger;
        }
    }
}
