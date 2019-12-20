using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using SFB;

public static class NodeEditor
{
    public enum IOTYPE { ANY, INTEGER, FLOAT, VEC2, VEC3, ROOM, DUNGEONOBJ }
    public static LayerMask releaseLineLayers;

    public static void Compile()
    {
        //Refresh all nodes and lines to have their values read
        if (Refresh())
        {
            //Initialize buffer for data to be written to the file
            string tempResult = "";

            bool failedCompilation = false;

            GameObject[] start = GameObject.FindGameObjectsWithTag("StartRoom");
            if(start.Length != 1)
            {
                ErrorLogger.ThrowErrorMessage("Dungeon layout must have 1 start room.");
                failedCompilation = true;
            }
            Room_Container currentRoom = start[0].GetComponent<Room_Container>();

            while(currentRoom.nextRoomOutput != null)
            {
                object result = currentRoom.Evaluate();
                if (result != null)
                    tempResult += (string)result + "\n";
                else
                {
                    failedCompilation = true;
                    /*Originally did not break out of the while loop, so as to log errors for all nodes before quitting.
                    However, in an attempt to clear some game-breaking bugs, I've sacrificed creature comforts for code stability.
                    therefore,*/ break;
                }

                currentRoom = currentRoom.nextRoomOutput.lineReferences[0].end.attachedNode.GetComponent<Room_Container>();
            }

            if (failedCompilation)
            {
                ErrorLogger.ThrowErrorMessage("Compilation failed.");
                return;
            }

            string path = StandaloneFileBrowser.SaveFilePanel("Save Dungeon", "", "", "jon");
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(tempResult);

            writer.Flush();
            writer.Close();
            
        }
        ErrorLogger.ThrowSuccessMessage("Compilation succeeded!");
    }

    public static bool Refresh()
    {
        //Find all objects of type Node_Generic, then convert that array into a list
        List<Node_Generic> nodes = new List<Node_Generic>(Object.FindObjectsOfType<Node_Generic>());
        LineConnector[] lines = Object.FindObjectsOfType<LineConnector>();

        for (int i = 0; i < nodes.Count; i++)
        {
            if (!nodes[i].Refresh())
                return false;
        }

        //Update the line collider
        for (int i = 0; i < lines.Length; i++)
            lines[i].Refresh();

        return true;
    }
}