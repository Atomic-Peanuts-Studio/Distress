using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<GameObject> ladders;
    public List<GameObject> enemies;

    public bool isLevelComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
