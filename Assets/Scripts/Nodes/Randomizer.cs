using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Randomizer : MonoBehaviour
{
    public TMP_InputField inputField;
    public NodeOutput output;
    
    void Update()
    {
        int.Parse(inputField.text);
        output.value = int.Parse(inputField.text);
    }
    
}