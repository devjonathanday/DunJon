using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public static class NodeEditor
{
    public enum IOTYPE { ANY, INTEGER, FLOAT, VEC2, VEC3, ROOM, DUNGEONOBJ }
    public static LayerMask releaseLineLayers;

    public static void Compile()
    {
        //Refresh all nodes and lines to have their values read
        if (Refresh())
        {
            //Get all nodes in the scene
            List<Room_Container> nodes = new List<Room_Container>(Object.FindObjectsOfType<Room_Container>());
            //StreamWriter writer = new StreamWriter(new FileStream(Application.streamingAssetsPath + "/dungeon.txt", FileMode.OpenOrCreate, FileAccess.Write));
            StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/dungeon.txt", false);

            bool failedCompilation = false;

            for (int i = 0; i < nodes.Count; i++)//Iterate through all the nodes
            {
                if (nodes[i].Evaluate() != null)
                    writer.WriteLine((string)nodes[i].Evaluate());
                else failedCompilation = true;
            }
            
            writer.Flush();
            writer.Close();

            if (failedCompilation)
            {
                ErrorLogger.ThrowErrorMessage("Compilation failed.");
                return;
            }
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