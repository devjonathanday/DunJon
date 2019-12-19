using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Node_Generic : MonoBehaviour, IPointerClickHandler
{
    public List<NodeInput> inputs = new List<NodeInput>();
    public List<NodeOutput> outputs = new List<NodeOutput>();

    public abstract bool Refresh();
    public abstract object Evaluate();
    public abstract string GetSaveData();
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
}