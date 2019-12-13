using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class NodeOutput : MonoBehaviour, IPointerDownHandler
{
    Camera cam;
    [SerializeField] public object value;
    public NodeEditor.IOTYPE outputType;
    public LineConnector linePrefab;
    public List<LineConnector> lineReferences = new List<LineConnector>();
    public Node_Generic attachedNode;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //Check if:
        // - Mouse button is released
        // - There is one or more lines connected to this output
        // - The last line (most recently created) is not yet finished
        if (Input.GetMouseButtonUp(0) && lineReferences.Count > 0 && !lineReferences[lineReferences.Count - 1].finished)
        {
            CheckMouseObject();
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LineConnector newLine = Instantiate(linePrefab);
            lineReferences.Add(newLine);
            newLine.StartLine(this);
        }
    }

    void CheckMouseObject()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 1000))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out NodeInput nodeInput))
            {
                if (nodeInput.inputType == NodeEditor.IOTYPE.ANY)
                {
                    nodeInput.value = value;
                    nodeInput.used = true;
                    lineReferences[lineReferences.Count - 1].FinishLine(nodeInput);
                    NodeEditor.Refresh();
                    return;
                }
                else
                {
                    if (!nodeInput.used)
                    {
                        if (outputType == nodeInput.inputType)
                        {
                            nodeInput.value = value;
                            nodeInput.used = true;
                            lineReferences[lineReferences.Count - 1].FinishLine(nodeInput);
                            NodeEditor.Refresh();
                            return;
                        }
                        else ErrorLogger.ThrowErrorMessage("Output type does not match input type.");
                    }
                    else ErrorLogger.ThrowErrorMessage("Input is already connected.");
                }
            }
        }
        Destroy(lineReferences[lineReferences.Count - 1].gameObject);
        lineReferences.RemoveAt(lineReferences.Count - 1);
        NodeEditor.Refresh();
    }

    public void DeleteLines()
    {
        lineReferences.Clear();
    }
}