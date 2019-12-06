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
    public LineConnector lineReference;

    void Start()
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
                if(nodeInput.inputType == NodeEditor.IOTYPE.ANY)
                {
                    nodeInput.value = value;
                    nodeInput.used = true;
                    nodeInput.inputType = outputType;
                    lineReference.FinishLine(nodeInput);
                    return;
                }
                else if (!nodeInput.used && outputType == nodeInput.inputType)
                {
                    nodeInput.value = value;
                    nodeInput.used = true;
                    lineReference.FinishLine(nodeInput);
                    return;
                }
            }
        }
        Destroy(lineReference.gameObject); //Destroy instead of DeleteLine(), because LineConnector's references are not populated yet
    }
}