using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonIO : MonoBehaviour
{
    void Awake()
    {
        //Init new StreamReader
        //Iterate through rooms
        //Add them to the list
        //Spawn everything using RoomGenerator.CreateRoom(),
        //then reposition them based on sizes as described below
    }
    
    void Update()
    {
        
    }
}

//TODO figure out object spawning inside rooms, minimum room size is 5, max is 20
//Parse through rooms, create list including sizes and objects
//iterate through the list, select random forward left or right direction to spawn rooms
//Resize walls and doors to fit
//Add hallways in between rooms (1 tile gap)
//Create dungeon map
//Spawn enemies and player accordingly

//Room positioning is (1/2 of first room size) + (1/2 of second room size) + (1 for gap between rooms)