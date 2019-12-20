using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject skeletonPrefab;
    public GameObject zombiePrefab;
    [Header("Environment")]
    public Transform frontWall;
    public Transform leftWall;
    public Transform rightWall;
    public Transform backWall;

    void Start()
    {

    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
            CreateRoom(5, 5, new GameObject[0]);
    }

    void CreateRoom(int sizeX, int sizeY, GameObject[] objects)
    {
        frontWall.position = (Vector3.forward  * ((float)sizeY / 2)) + (Vector3.up * 10);
        leftWall.position =  (-Vector3.right   * ((float)sizeX / 2)) + (Vector3.up * 10);
        rightWall.position = (Vector3.right    * ((float)sizeX / 2)) + (Vector3.up * 10);
        backWall.position =  (-Vector3.forward * ((float)sizeY / 2)) + (Vector3.up * 10);
    }
}

//TODO figure out object spawning inside rooms, minimum room size is 3, max is 20