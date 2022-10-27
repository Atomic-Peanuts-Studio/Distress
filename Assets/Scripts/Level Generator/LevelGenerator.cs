using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<GameObject> _RoomPoolCopy;
    public int RoomsInLevel = 3;
    public bool multiPath = true;
    public bool chanceRooms = true;
    public int chance = 30;

    [Header("Public Variables")]
    private float PosX = 0f;
    private float PosY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _RoomPoolCopy = Rooms;

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

    private void generateSinglePath()
    {
        for (int i = 0; i < RoomsInLevel; i++)
        {
            // Select the rooms
            int random = Random.Range(0, _RoomPoolCopy.Count - 1);
            Instantiate(_RoomPoolCopy[random], new Vector2(PosX + 50, PosY), Quaternion.identity);
            PosX += 50;
            _RoomPoolCopy.Remove(_RoomPoolCopy[random]);

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
            Debug.Log(_RoomPoolCopy.Count);
            // Select the rooms
            int randomRoom1 = Random.Range(0, _RoomPoolCopy.Count - 1);
            Instantiate(_RoomPoolCopy[randomRoom1], new Vector2(PosX + 50, PosY + 50), Quaternion.identity);
            _RoomPoolCopy.Remove(_RoomPoolCopy[randomRoom1]);

            int randomRoom2 = Random.Range(0, _RoomPoolCopy.Count - 1);
            Instantiate(_RoomPoolCopy[randomRoom2], new Vector2(PosX + 50, PosY - 50), Quaternion.identity);
            _RoomPoolCopy.Remove(_RoomPoolCopy[randomRoom2]);
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
            int chanceRoom = Random.Range(0, _RoomPoolCopy.Count - 1);
            Instantiate(_RoomPoolCopy[chanceRoom], new Vector2(PosX, PosY), Quaternion.identity);
            _RoomPoolCopy.Remove(_RoomPoolCopy[chanceRoom]);
        }
    }
}