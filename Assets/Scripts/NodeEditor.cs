using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NodeEditor
{
    public enum IOTYPE { ANY, INTEGER, FLOAT, VEC2, VEC3, ROOM, DUNGEONOBJ }

    public static void Compile()
    {
        Refresh();
    }

    public static void Refresh()
    {
        //Find all objects of type Node_Generic, then convert that array into a list
        List<Node_Generic> nodes = new List<Node_Generic>(Object.FindObjectsOfType<Node_Generic>());
        nodes.Sort();
        LineConnector[] lines = Object.FindObjectsOfType<LineConnector>();

        for (int i = 0; i < nodes.Count; i++)
            nodes[i].Refresh();

        //Update the line collider
        for (int i = 0; i < lines.Length; i++)
            lines[i].Refresh();
    }
}
