using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Value_Integer : Node_Generic
{
    public TMP_InputField inputField;

    public override object Evaluate()
    {
        Refresh();
        return int.Parse(inputField.text);
    }

    public override bool Refresh()
    {
        if (outputs.Count == 0)
        {
            ErrorLogger.ThrowErrorMessage("An integer node has no output node assiged.");
            return false;
        }
        return true;
    }
}