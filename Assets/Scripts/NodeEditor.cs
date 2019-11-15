using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeEditor
{
    public enum IOTYPE { INTEGER, FLOAT, VEC2, VEC3 }
    public static CommonContainer FindCommonContainer()
    {
        return GameObject.FindGameObjectWithTag("CommonContainer").GetComponent<CommonContainer>();
    }
}
