using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class NodeOutput : Button, IPointerDownHandler
{
    Camera cam;
    [SerializeField] public object value;
    public NodeEditor.IOTYPE outputType;
    public LineConnector linePrefab;
    [SerializeField] LineConnector lineReference;

    protected override void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && lineReference != null && !lineReference.finished)
        {
            CheckMouseObject();
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerDown(eventData);
            lineReference = Instantiate(linePrefab);
            lineReference.StartLine(this);
        }
    }

    void CheckMouseObject()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 1000))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out NodeInput nodeInput))
            {
                if (outputType == nodeInput.inputType)
                {
                    nodeInput.value = value;
                    lineReference.FinishLine(nodeInput);
                    return;
                }
            }
        }
        Destroy(lineReference.gameObject);
    }
}

[CustomEditor(typeof(NodeOutput))]
public class NodeOutputEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}