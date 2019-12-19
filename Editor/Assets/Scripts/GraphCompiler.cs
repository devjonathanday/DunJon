using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphCompiler : MonoBehaviour
{
    public void Compile()
    {
        NodeEditor.Compile();
    }
    public void SaveGraph()
    {
        NodeEditor.SaveGraph();
    }
}