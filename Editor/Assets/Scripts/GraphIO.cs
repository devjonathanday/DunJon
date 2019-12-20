using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GraphIO : MonoBehaviour
{
    public GameObject node_integer, node_room, node_randomizer, node_skeleton, node_zombie, node_start, node_end;
    public GameObject lineConnectorPrefab;
    public Transform worldCanvas;

    class LineInfo
    {
        public int startNodeID;
        public int startIndex;
        public int endNodeID;
        public int endIndex;
    }

    public void SaveGraph()
    {
        //Get all the nodes in the scene, put them in a list
        Node_Generic[] nodes = FindObjectsOfType<Node_Generic>();
        Dictionary<LineConnector, LineInfo> lines = new Dictionary<LineConnector, LineInfo>();

        //Iterate through the nodes
        for (int i = 0; i < nodes.Length; i++)
        {
            //Iterate through the inputs of the node
            for (int k = 0; k < nodes[i].inputs.Count; k++)
            {
                //See if the input has a line connected
                if (nodes[i].inputs[k].lineReference != null)
                {
                    //Check the list of lines for that lineReference
                    if (lines.ContainsKey(nodes[i].inputs[k].lineReference))
                    {
                        lines[nodes[i].inputs[k].lineReference].endNodeID = i;
                        lines[nodes[i].inputs[k].lineReference].endIndex = k;
                    }
                    //If the line does not exist in the dictionary yet
                    else
                    {
                        //Create the LineInfo for it, add it to the list
                        LineInfo newInfo = new LineInfo();
                        newInfo.endNodeID = i;
                        newInfo.endIndex = k;
                        lines.Add(nodes[i].inputs[k].lineReference, newInfo);
                    }
                }
            }
            //Iterate through the outputs of the node
            for (int k = 0; k < nodes[i].outputs.Count; k++)
            {
                //Iterate through the lines connected to that output
                for (int m = 0; m < nodes[i].outputs[k].lineReferences.Count; m++)
                {
                    //Check the list of lines for that lineReference
                    if (lines.ContainsKey(nodes[i].outputs[k].lineReferences[m]))
                    {
                        lines[nodes[i].outputs[k].lineReferences[m]].startNodeID = i;
                        lines[nodes[i].outputs[k].lineReferences[m]].startIndex = k;
                    }
                    //If the line does not exist in the dictionary yet
                    else
                    {
                        //Create the LineInfo for it, add it to the list
                        LineInfo newInfo = new LineInfo();
                        newInfo.startNodeID = i;
                        newInfo.startIndex = k;
                        lines.Add(nodes[i].outputs[k].lineReferences[m], newInfo);
                    }
                }
            }
        }

        StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/graph.txt", false);

        for (int i = 0; i < nodes.Length; i++)
            writer.WriteLine("N," + i + "," + nodes[i].GetSaveData() + "," + nodes[i].transform.position.x + "," + nodes[i].transform.position.y);

        writer.Write("\n");

        foreach (KeyValuePair<LineConnector, LineInfo> entry in lines)
            writer.WriteLine("L," + entry.Value.startNodeID + "," + entry.Value.startIndex + "," + entry.Value.endNodeID + "," + entry.Value.endIndex);

        writer.Flush();
        writer.Close();
    }

    public void LoadGraph()
    {
        if (File.Exists(Application.streamingAssetsPath + "/graph.txt"))
        {
            StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/graph.txt");

            //Delete all existing nodes before spawning new ones
            Node_Generic[] tempNodes = FindObjectsOfType<Node_Generic>();
            for (int i = 0; i < tempNodes.Length; i++)
                tempNodes[i].DeleteNode();
            
            List<Node_Generic> nodes = new List<Node_Generic>();

            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length == 0) continue;

                    if (line[0] == 'N')
                    {
                        string[] frags = line.Split(',');
                        switch(frags[2])
                        {
                            case "int":
                                {
                                    Value_Integer newNode = Instantiate(node_integer, new Vector3(float.Parse(frags[4]), float.Parse(frags[5]), 0), Quaternion.identity).GetComponent<Value_Integer>();
                                    newNode.inputField.text = frags[3];
                                    newNode.transform.SetParent(worldCanvas);
                                    nodes.Add(newNode);
                                }
                                break;
                            case "rom":
                                {
                                    Node_Generic newNode = Instantiate(node_room, new Vector3(float.Parse(frags[3]), float.Parse(frags[4]), 0), Quaternion.identity).GetComponent<Node_Generic>();
                                    newNode.transform.SetParent(worldCanvas);
                                    nodes.Add(newNode);
                                }
                                break;
                            case "str":
                                {
                                    Node_Generic newNode = Instantiate(node_start, new Vector3(float.Parse(frags[3]), float.Parse(frags[4]), 0), Quaternion.identity).GetComponent<Node_Generic>();
                                    newNode.transform.SetParent(worldCanvas);
                                    nodes.Add(newNode);
                                }
                                break;
                            case "end":
                                {
                                    Node_Generic newNode = Instantiate(node_end, new Vector3(float.Parse(frags[3]), float.Parse(frags[4]), 0), Quaternion.identity).GetComponent<Node_Generic>();
                                    newNode.transform.SetParent(worldCanvas);
                                    nodes.Add(newNode);
                                }
                                break;
                            case "rng":
                                {
                                    Randomizer newNode = Instantiate(node_randomizer, new Vector3(float.Parse(frags[4]), float.Parse(frags[5]), 0), Quaternion.identity).GetComponent<Randomizer>();
                                    newNode.transform.SetParent(worldCanvas);
                                    newNode.Resize(int.Parse(frags[3]));

                                    nodes.Add(newNode);
                                }
                                break;
                            case "skl":
                                {
                                    Node_Enemy newNode = Instantiate(node_skeleton, new Vector3(float.Parse(frags[4]), float.Parse(frags[5]), 0), Quaternion.identity).GetComponent<Node_Enemy>();
                                    newNode.health = int.Parse(frags[3]);
                                    newNode.enemyType = Node_Enemy.ENEMYTYPE.SKELETON;
                                    newNode.transform.SetParent(worldCanvas);
                                    nodes.Add(newNode);
                                }
                                break;
                            case "zmb":
                                {
                                    Node_Enemy newNode = Instantiate(node_zombie, new Vector3(float.Parse(frags[4]), float.Parse(frags[5]), 0), Quaternion.identity).GetComponent<Node_Enemy>();
                                    newNode.health = int.Parse(frags[3]);
                                    newNode.enemyType = Node_Enemy.ENEMYTYPE.ZOMBIE;
                                    newNode.transform.SetParent(worldCanvas);
                                    nodes.Add(newNode);
                                }
                                break;
                            default:
                                {
                                    ErrorLogger.ThrowErrorMessage("Graph could not be loaded correctly.");
                                    return;
                                }
                        }
                    }
                    else if (line[0] == 'L')
                    {
                        string[] frags = line.Split(',');

                        //startNodeID = int.Parse(frags[1]);
                        //outputIndex = int.Parse(frags[2]);
                        //endNodeID   = int.Parse(frags[3]);
                        //inputIndex  = int.Parse(frags[4]);

                        LineConnector newLine = Instantiate(lineConnectorPrefab, Vector3.zero, Quaternion.identity).GetComponent<LineConnector>();
                        newLine.StartLine(nodes[int.Parse(frags[1])].outputs[int.Parse(frags[2])]);
                        newLine.FinishLine(nodes[int.Parse(frags[3])].inputs[int.Parse(frags[4])]);
                    }
                }

                NodeEditor.Refresh();
            }
            catch (System.Exception e)
            {
                ErrorLogger.ThrowErrorMessage(e.Message);
            }
        }
        else ErrorLogger.ThrowErrorMessage("Graph has not been saved, or does not exist.");
    }

    public void Compile()
    {
        NodeEditor.Compile();
    }
}

//TODO remove all nodes from the scene before instantiating new ones