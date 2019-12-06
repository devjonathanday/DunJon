using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Randomizer : MonoBehaviour
{
    public TMP_InputField inputField;
    public NodeInput nodeInputPrefab;
    public NodeOutput output;
    public List<NodeInput> inputs = new List<NodeInput>();
    [Space(10)]
    public Vector2 startInputPos;
    public float inputSpacing;
    public float defaultNodeHeight;
    public float nodeHeightIncrement;

    private void Awake()
    {
        UpdateNodeInputs();
    }
    public void UpdateNodeInputs()
    {
        int count = int.Parse(inputField.text);
        if (count < 2)
        {
            inputField.text = "2";
            count = 2;
        }
        while(inputs.Count > 0)
        {
            if(inputs[0].lineReference) inputs[0].lineReference.DeleteLine();
            Destroy(inputs[0].gameObject);
            inputs.Remove(inputs[0]);
        }
        for (int i = 0; i < count; i++)
        {
            NodeInput newNode = Instantiate(nodeInputPrefab, Vector2.zero, Quaternion.identity);
            newNode.transform.SetParent(transform);

            RectTransform newNodeRect = newNode.GetComponent<RectTransform>();
            newNodeRect.anchorMin = Vector2.zero;
            newNodeRect.anchorMax = Vector2.zero;
            newNodeRect.anchoredPosition = startInputPos + (Vector2.up * i * inputSpacing);
            
            inputs.Add(newNode);
        }
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultNodeHeight + (nodeHeightIncrement * count));
    }
    void Evaluate()
    {
        output.value = UnityEngine.Random.Range(0, 1);
    }
}