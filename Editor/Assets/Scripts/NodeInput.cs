using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInput : MonoBehaviour
{
    public object value;
    public bool used;
    public NodeEditor.IOTYPE inputType;
    public LineConnector lineReference;
    public Node_Generic attachedNode;
}