using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<GameObject> ladders;
    public List<GameObject> enemies;

    public bool isLevelComplete = false;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }

        if(enemies.Count <= 0)
        {
            isLevelComplete = true;
        }

        if(isLevelComplete)
        {
            for(int i = 0; i < ladders.Count; i++)
            {
                ladders[i].SetActive(true);
            }
        }
    }
}
