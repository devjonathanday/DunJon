using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Value_Integer : MonoBehaviour
{
    public TMP_InputField inputField;
    public NodeOutput output;
    
    void Update()
    {
        output.value = int.Parse(inputField.text);
    }
}