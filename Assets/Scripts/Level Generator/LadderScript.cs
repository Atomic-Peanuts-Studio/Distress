using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private GameObject LevelManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerAttribute>() != null && this.gameObject.transform.parent.gameObject.GetComponent<RoomScript>().isLevelComplete)
        {
            GameObject newRoom = LevelManager.GetComponent<LevelGenerator>().NextRoom();
            Vector3 newPosition = newRoom.transform.GetChild(0).gameObject.transform.position;
            collision.gameObject.transform.position = newPosition;

            Destroy(this.gameObject.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("LevelGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
