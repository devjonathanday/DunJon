using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public override bool Refresh()
    {
        #region Resizing
        
        int count = int.Parse(inputField.text);
        if (count < 2)
        {
            inputField.text = "2";
            count = 2;
            ErrorLogger.ThrowErrorMessage("Minimum input count for Randomizer is 2.");
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

        #endregion

        #region Process Inputs

        NodeEditor.IOTYPE inputType = NodeEditor.IOTYPE.ANY;

        for (int i = 0; i < inputs.Count; i++)
        {
            if (!inputs[i].used) //See if we have empty NodeInputs
            {
                //Abort
                ErrorLogger.ThrowErrorMessage("Randomizer node is missing inputs.");
                return false;
            }
            else if (i != 0)
            {
                if (inputs[i].inputType != inputType) //Check if we have mismatched input types
                {
                    //Abort
                    ErrorLogger.ThrowErrorMessage("Randomizer node is missing inputs.");
                    return false;
                }
            }
            else inputType = inputs[i].inputType; //Set the inputType to the first input, if applicable
        }
        return true; //Success

        #endregion
    }

    public override object Evaluate()
    {
        if (Refresh())
        {
            //Initialize list of objects to be chosen from
            List<object> inputObjects = new List<object>();
            //Add the evaluated value of the input to the list
            for (int i = 0; i < inputs.Count; i++)
                inputObjects.Add(inputs[i].lineReference.start.attachedNode.Evaluate());
            //Choose an input 'randomly'
            return inputObjects[Random.Range(0, inputs.Count)];
        }
        else return null;
    }
}