using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Container : MonoBehaviour
{
    public NodeInput widthInput;
    public NodeInput heightInput;
    int width;
    int height;

    void Start()
    {
        
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            width = (int)widthInput.value;
            height = (int)heightInput.value;
            Debug.Log("Room Size = (" + width + "," + height + ")");
        }
    }
}