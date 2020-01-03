using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject skeletonPrefab;
    public GameObject zombiePrefab;

    [Header("Environment")]
    public GameObject roomPrefab;


    void Start()
    {
        DungeonIO loader = GameObject.FindGameObjectWithTag("DungeonLoader").GetComponent<DungeonIO>();
        Vector3 spawnPos = Vector3.zero;

        for (int i = 0; i < loader.rooms.Count; i++)
        {
            switch(Random.Range(0, 3))
            {
                case 0:
                    spawnPos.x += 20;
                    break;
                case 1:
                    spawnPos.x -= 20;
                    break;
                case 3:
                    spawnPos.z += 20;
                    break;
            }
            RoomContainer newRoom = Instantiate(roomPrefab, spawnPos, Quaternion.identity).GetComponent<RoomContainer>();
            CreateRoom(newRoom, (int)loader.rooms[i].size.x, (int)loader.rooms[i].size.y, loader.rooms[i].enemies);
        }
    }

    void CreateRoom(RoomContainer room, int sizeX, int sizeY, List<Enemy> enemies)
    {
        room.frontWall.position  += (Vector3.forward  * ((float)sizeY / 2)) + (Vector3.up * 10);
        //room.frontWall.localScale = new Vector3(sizeX, 1, 1);

        room.leftWall.position   += (-Vector3.right   * ((float)sizeX / 2)) + (Vector3.up * 10);
        //room.leftWall.localScale  = new Vector3(sizeY, 1, 1);

        room.rightWall.position  += (Vector3.right    * ((float)sizeX / 2)) + (Vector3.up * 10);
        //room.rightWall.localScale = new Vector3(sizeY, 1, 1);

        room.backWall.position   += (-Vector3.forward * ((float)sizeY / 2)) + (Vector3.up * 10);
        //room.backWall.localScale  = new Vector3(sizeX, 1, 1);
    }
}