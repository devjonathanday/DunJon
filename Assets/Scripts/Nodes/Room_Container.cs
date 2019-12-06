using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Container : Node_Generic
{
    [SerializeField] int width;
    [SerializeField] int height;

    public override void Refresh()
    {
        if (inputs.Count == 0)
            Debug.LogError("Room Node " + gameObject.GetInstanceID() + " has missing parameters.");

        if (inputs[0].value != null) width = (int)inputs[0].value;
        if (inputs[1].value != null) height = (int)inputs[1].value;
    }
}