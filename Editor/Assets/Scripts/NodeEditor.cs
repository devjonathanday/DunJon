using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
            StreamWriter writer = new StreamWriter(new FileStream(Application.streamingAssetsPath + "/dungeon.txt", FileMode.OpenOrCreate, FileAccess.Write));

            bool failedCompilation = false;

            for (int i = 0; i < nodes.Count; i++)//Iterate through all the nodes
            {
                if (nodes[i].Evaluate() != null)
                    writer.WriteLine((string)nodes[i].Evaluate());
                else failedCompilation = true;
            }

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

    class LineInfo
    {
        public int startNodeID;
        public int startIndex;
        public int endNodeID;
        public int endIndex;
    }

    public static string SaveGraph()
    {
        //Get all the nodes in the scene, put them in a list
        Node_Generic[] nodes = Object.FindObjectsOfType<Node_Generic>();
        
        //Iterate through the nodes
        for (int i = 0; i < nodes.Length; i++)
        {
            //Iterate through the inputs of the node
            for (int k = 0; k < nodes[i].inputs.Count; k++)
            {
                //See if the input has a line connected
                //  Check the list of lines for that lineReference
                //      Set the line's endNodeID to i
                //      Set the line's endIndex to k
                //  If the line does not exist yet
                //      Create the line, add it to the list
                //      Set the line's endNodeID to i
                //      Set the line's endIndex to k
            }
            //Iterate through the outputs of the node
            for (int k = 0; k < nodes[i].outputs.Count; k++)
            {
                //Iterate through the lines connected to that output
                for (int m = 0; m < nodes[i].outputs[k].lineReferences.Count; m++)
                {

                }
            }
        }



        //Dictionary<int, int> nodeDictionary = new Dictionary<int, int>();
        

        ////Get all the nodes in the scene
        //Node_Generic[] nodes = Object.FindObjectsOfType<Node_Generic>();
        ////Iterate through all nodes in the scene
        //for (int i = 0; i < nodes.Length; i++)
        //{
        //    //Add this node to the dictionary with key = InstanceID
        //    nodeDictionary.Add(nodes[i].GetInstanceID(), i);
        //    //Iterate through the inputs of the node
        //    for (int k = 0; k < nodes[i].inputs.Count; k++)
        //    {
        //        //Check if there is a line attached to it
        //        if (nodes[i].inputs[k].lineReference != null)
        //        {
        //            LineInfo info;
        //            //If that line already exists in the line dictionary
        //            if (lineDictionary.TryGetValue(nodes[i].inputs[k].lineReference.GetInstanceID(), out info))
        //            {
        //                //Set the line's endNodeID to the node's InstanceID
        //                nodeDictionary.TryGetValue(nodes[i].GetInstanceID(), out lineDictionary[nodes[i].inputs[k].lineReference.GetInstanceID()].endNodeID);
        //                //Set the line's endNodeIndex to which input index we're on
        //                lineDictionary[nodes[i].inputs[k].lineReference.GetInstanceID()].endIndex = k;
        //            }
        //            //if the line does not exist in the dictionary
        //            else
        //            {
        //                //Set the line's endNodeID to the node's InstanceID
        //                info.endNodeID = nodes[i].GetInstanceID();
        //                //Set the line's endNodeIndex to which input index we're on
        //                info.endIndex = k;
        //                //Add the new line to the dictionary
        //                lineDictionary.Add(nodes[i].inputs[k].lineReference.GetInstanceID(), info);
        //            }
        //        }
        //    }
        //    //Iterate through the outputs of the node
        //    for (int k = 0; k < nodes[i].outputs.Count; k++)
        //    {
        //        //Iterate through the lines attached to output k
        //        for (int m = 0; m < nodes[i].outputs[k].lineReferences.Count; m++)
        //        {
        //            LineInfo info;
        //            //If that line already exists in the line dictionary
        //            if (lineDictionary.TryGetValue(nodes[i].outputs[k].lineReferences[m].GetInstanceID(), out info))
        //            {
        //                //Set the line's endNodeID to the node's InstanceID
        //                nodeDictionary.TryGetValue(nodes[i].GetInstanceID(), out lineDictionary[nodes[i].outputs[k].lineReferences[m].GetInstanceID()].startNodeID);
        //                //Set the line's endNodeIndex to which input index we're on
        //                lineDictionary[nodes[i].outputs[k].lineReferences[m].GetInstanceID()].startIndex = k;
        //            }
        //            //if the line does not exist in the dictionary
        //            else
        //            {
        //                //Set the line's endNodeID to the node's InstanceID
        //                info.startNodeID = nodes[i].GetInstanceID();
        //                //Set the line's endNodeIndex to which input index we're on
        //                info.startIndex = k;
        //                //Add the new line to the dictionary
        //                lineDictionary.Add(nodes[i].outputs[k].lineReferences[m].GetInstanceID(), info);
        //            }
        //        }
        //    }
        //}

        //StreamWriter writer = new StreamWriter(new FileStream(Application.streamingAssetsPath + "/graph.txt", FileMode.OpenOrCreate, FileAccess.Write));

        //foreach (KeyValuePair<int, int> entry in nodeDictionary)
        //{
        //    //writer.WriteLine("(" + entry.Value + ")" + GameObject.find)
        //}


        //for (int i = 0; i < nodes.Length; i++)
        //    writer.WriteLine(nodes[i].GetSaveData());

        return string.Empty;
    }
}

//TODO don't forget to save the position of the nodes too!
//Add the nodes to an array, keep track of their index in the array