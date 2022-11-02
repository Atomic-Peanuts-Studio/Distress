using LDtkUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> Rooms;
    public GameObject SpawnRoom;
    public GameObject BossRoom;

    private int roomsCounter;
    private int mainRoomsCreated;

    public int maxMainRooms = 5;
    public int chance = 30;
    public int maxRooms = 6;

    [Header("Public Variables")]
    private float PosX = 0f;
    private float PosY = 0f;

    void Start()
    {
        roomsCounter = 0;
        mainRoomsCreated = 0;
    }

    public GameObject NextRoom()
    {
        if(roomsCounter > maxRooms || mainRoomsCreated >= maxMainRooms)
        {
            Instantiate(BossRoom, new Vector2(PosX, PosY), Quaternion.identity);
            return BossRoom;
        }
        else
        {
            return GenerateRoom();
        }
    }

    private GameObject GenerateRoom()
    {
        int randomRoom = Random.Range(0, Rooms.Count-1);

        Instantiate(Rooms[randomRoom], new Vector2(PosX, PosY), Quaternion.identity);
        roomsCounter++;

        CalculateSideRoom();

        return Rooms[randomRoom];
    }

    private void CalculateSideRoom()
    {
        int sideRoomChance = Random.Range(0, 100);
        if (sideRoomChance > chance)
        {
            mainRoomsCreated++;
        }

        Debug.Log(mainRoomsCreated);
    }

    public void Restart()
    {
        roomsCounter = 0;
        mainRoomsCreated = 0;
    }
}