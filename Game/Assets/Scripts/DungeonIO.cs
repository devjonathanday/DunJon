using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using System.IO;

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

    void LoadDungeon()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Load Dungeon", "", "jon", false);
        if (paths.Length == 0)
        {
            //Throw "file not selected" error. Or, just do nothing
            return;
        }
        else
        {
            StreamReader reader = new StreamReader(paths[0]);
            string line;
            while((line = reader.ReadLine()) != null)
            {
                if (line.Length == 0) continue;

                string[] frags = line.Split(',');

                switch(frags[0])
                {
                    case "str":
                        break;
                    case "rom":
                        break;
                    case "end":
                        break;
                }
            }
        }
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