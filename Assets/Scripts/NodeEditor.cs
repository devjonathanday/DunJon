using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NodeEditor
{
    public enum IOTYPE { ANY, INTEGER, FLOAT, VEC2, VEC3, ROOM, DUNGEONOBJ }
    public static LayerMask releaseLineLayers;

    public static void Compile()
    {
        //Refresh all nodes and lines to have their values read
        Refresh();

        //Get all nodes in the scene
        List<Node_Generic> nodes = new List<Node_Generic>(Object.FindObjectsOfType<Node_Generic>());

        for (int i = 0; i < nodes.Count; i++) //Iterate through all the nodes
            for (int k = 0; k < nodes[i].outputs.Count; k++) //Iterate through its outputs
                if (nodes[i].outputs[k].lineReferences.Count == 0) //See if an output has no lines connecting it
                {
                    ErrorLogger.ThrowErrorMessage("Compile failed: All existing nodes must have outputs connected.");
                    return;
                }
    
        bool allFulfilled = true; //Represents if every node has been checked and NodeInputs are in their final state
        
        //Set all nodes' fulfilled status to false
        for (int i = 0; i < nodes.Count; i++)
            if (!nodes[i].fulfilled)
                allFulfilled = false;

        while (!allFulfilled)
        {
            allFulfilled = true;
            for (int i = 0; i < nodes.Count; i++)
                if (!nodes[i].fulfilled)
                    allFulfilled = false;

            for (int i = 0; i < nodes.Count; i++) //Iterate through all the nodes
            {
                if (nodes[i].priority == 0) //If this node is at the end of the chain (0 priority)
                {
                    for (int k = 0; k < nodes[i].outputs.Count; k++) //Iterate through its outputs
                    {
                        for (int m = 0; m < nodes[i].outputs[k].lineReferences.Count; m++) //Iterate through the lines connected to that output
                        {
                            nodes[i].outputs[k].lineReferences[m].end.value = nodes[i].outputs[k].lineReferences[m].start.value; //Set the end value of the line equal to the beginning node's value
                        }
                    }
                    nodes[i].fulfilled = true;
                }
            }
        }
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