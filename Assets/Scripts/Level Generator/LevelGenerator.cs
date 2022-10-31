using LDtkUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

// Some test code, a lot needs to change
public class LevelGenerator : MonoBehaviour
{
    /*
     * Generator Algorithm: Composites
     * 1. Start with a fixed (Rest) room
     * 2. Generate a Composite level (Consists of 3 room)
     * 3. Connect the Composite level to Rest and boss room.
     */
    public List<GameObject> Rooms;
    public GameObject SpawnRoom;
    public GameObject BossRoom;
    //private GameObject currentRoom;

    private int roomsCounter;
    private int mainRoomsCreated;
    private int previousRoomIdentifier;

    public int maxMainRooms = 5;
    public int chance = 30;

    [Header("Public Variables")]
    private float PosX = 0f;
    private float PosY = 0f;

    void Start()
    {
        //currentRoom = SpawnRoom;
        roomsCounter = 0;
        mainRoomsCreated = 0;
        previousRoomIdentifier = -1;
    }

    public GameObject NextRoom()
    {
        if(roomsCounter > 6 || mainRoomsCreated >= maxMainRooms)
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
        int randomRoom = Random.Range(0, Rooms.Count - 1);
        /*
        while (randomRoom == previousRoomIdentifier)
        {
            randomRoom = Random.Range(0, Rooms.Count - 1);
        }
        */
        previousRoomIdentifier = randomRoom;
        //PosX += 50;
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

    private void Restart()
    {
        roomsCounter = 0;
        mainRoomsCreated = 0;
        previousRoomIdentifier = -1;
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SpawnRoom.GetComponentsInChildren<Transform>()[4]);

        PosX = SpawnRoom.transform.position.x;
        PosY = SpawnRoom.transform.position.y;

        if(multiPath)
        {
            generateMultiPath();
        }
        else
        {
            generateSinglePath();
        }

        Instantiate(BossRoom, new Vector2(PosX + 50, PosY), Quaternion.identity);
    }

    private void Update()
    {
    }

    
     private void generateSinglePath()
    {
        for (int i = 0; i < RoomsInLevel; i++)
        {
            // Select the rooms
            int random = Random.Range(0, Rooms.Count - 1);
            Instantiate(Rooms[random], new Vector2(PosX + 50, PosY), Quaternion.identity);
            PosX += 50;

            if(chanceRooms)
            {
                generateChanceRoom(PosX, PosY + 50);
                generateChanceRoom(PosX, PosY - 50);
            }
        }
    }

    private void generateMultiPath()
    {   
        for (int i = 0; i < RoomsInLevel; i++)
        {
            Debug.Log(Rooms.Count);
            // Select the rooms
            int randomRoom1 = Random.Range(0, Rooms.Count - 1);
            Instantiate(Rooms[randomRoom1], new Vector2(PosX + 50, PosY + 50), Quaternion.identity);
            //generatedRooms.Add(randomRoom1);

            int randomRoom2 = Random.Range(0, Rooms.Count - 1);
            Instantiate(Rooms[randomRoom2], new Vector2(PosX + 50, PosY - 50), Quaternion.identity);
            //generatedRooms.Add(randomRoom2);
            PosX += 50;

            if (chanceRooms)
            {
                generateChanceRoom(PosX, PosY + 100);
                generateChanceRoom(PosX, PosY - 100);
            }
        }
    } 
     

    private void generateChanceRoom(float PosX, float PosY)
    {
        int randomChance = Random.Range(0, 100);
        if (randomChance <= chance)
        {
            int chanceRoom = Random.Range(0, Rooms.Count - 1);
            Instantiate(Rooms[chanceRoom], new Vector2(PosX, PosY), Quaternion.identity);
            //generatedRooms.Add(randomChance);
        }
    }

    public void OnLDtkImportFields(LDtkFields fields)
    {
    }
    */
}