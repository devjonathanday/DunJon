using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Value_Integer : Node_Generic
{
    public TMP_InputField inputField;

    public override void Refresh()
    {
        if (outputs.Count == 0)
            Debug.LogError("Integer Node " + gameObject.GetInstanceID() + " has no output node assiged.");
        else outputs[0].value = int.Parse(inputField.text);
    }
}