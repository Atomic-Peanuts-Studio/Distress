using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLadder : MonoBehaviour
{
    private GameObject LevelManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerAttribute>() != null && this.gameObject.transform.parent.gameObject.GetComponent<RoomScript>().isLevelComplete)
        {
            AudioSource _song = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            LevelManager.GetComponent<LevelGenerator>().Restart();
            Instantiate(LevelManager.GetComponent<LevelGenerator>().SpawnRoom, new Vector2(0,0), Quaternion.identity);
            Vector3 newPosition = LevelManager.GetComponent<LevelGenerator>().SpawnRoom.transform.GetChild(0).transform.position;
            collision.gameObject.transform.position = newPosition;
            _song.Play();

            Destroy(this.gameObject.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("LevelGenerator");
    }
}
