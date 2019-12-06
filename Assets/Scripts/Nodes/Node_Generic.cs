using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Node_Generic : MonoBehaviour, IComparable<Node_Generic>, IPointerClickHandler
{
    public List<NodeInput> inputs = new List<NodeInput>();
    public List<NodeOutput> outputs = new List<NodeOutput>();
    public int priority;

    public abstract void Refresh();
    public void DeleteNode()
    {
        for (int i = 0; i < inputs.Count; i++)
            if (inputs[i].lineReference != null) inputs[i].lineReference.DeleteLine();

        for (int i = 0; i < outputs.Count; i++)
            if(outputs[i].lineReferences.Count > 0) outputs[i].DeleteLines();

        Destroy(gameObject);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftAlt))
            DeleteNode();
    }

    public int CompareTo(Node_Generic other)
    {
        return priority.CompareTo(other.priority);
    }
}