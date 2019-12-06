using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Randomizer : Node_Generic
{
    public TMP_InputField inputField;
    public NodeInput nodeInputPrefab;
    [Space(10)]
    public Vector2 startInputPos;
    public float inputSpacing;
    public float defaultNodeHeight;
    public float nodeHeightIncrement;

    void Awake()
    {
        Refresh();
    }
    public override void Refresh()
    {
        int count = int.Parse(inputField.text);
        if (count < 2)
        {
            inputField.text = "2";
            count = 2;
        }
        while (inputs.Count > 0) //Iterate through the lines and delete them when changing size
        {
            if (inputs[0].lineReference) inputs[0].lineReference.DeleteLine();
            Destroy(inputs[0].gameObject);
            inputs.Remove(inputs[0]);
        }
        for (int i = 0; i < count; i++) //Set the positions of the input nodes
        {
            NodeInput newNode = Instantiate(nodeInputPrefab, Vector2.zero, Quaternion.identity);
            newNode.transform.SetParent(transform);

            RectTransform newNodeRect = newNode.GetComponent<RectTransform>();
            newNodeRect.anchorMin = Vector2.zero;
            newNodeRect.anchorMax = Vector2.zero;
            newNodeRect.anchoredPosition = startInputPos + (Vector2.up * i * inputSpacing);

            inputs.Add(newNode);
        }
        //Resize the node based on number of input nodes
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultNodeHeight + (nodeHeightIncrement * count));
    }
}