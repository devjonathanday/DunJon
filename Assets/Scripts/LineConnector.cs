using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineConnector : MonoBehaviour, IPointerClickHandler
{
    public LineRenderer line;
    public NodeOutput start;
    public NodeInput end;
    public bool finished;
    public CapsuleCollider collider;
    Camera cam;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        cam = Camera.main;
    }

    void Update()
    {
        if (!finished)
        {
            line.SetPosition(0, start.transform.position);
            line.SetPosition(1, cam.ScreenToWorldPoint(Input.mousePosition));
        }
        else
        {
            line.SetPosition(0, start.transform.position);
            line.SetPosition(1, end.transform.position);
            UpdateLineCollider();
        }
    }

    public void StartLine(NodeOutput startObject)
    {
        finished = false;
        start = startObject;
    }
    public void FinishLine(NodeInput endObject)
    {
        end = endObject;
        end.used = true;
        end.lineReference = this;
        end.value = start.value;
        finished = true;
    }
    public void DeleteLine()
    {
        start.value = null;
        end.value = null;

        end.used = false;

        start.lineReference = null;
        Destroy(gameObject);
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
            DeleteLine();
    }
    void UpdateLineCollider()
    {
        transform.position = (line.GetPosition(0) + line.GetPosition(1)) / 2;
        Vector3 lineVector = line.GetPosition(1) - line.GetPosition(0);
        transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(lineVector.y, lineVector.x), Vector3.forward);
        collider.height = (line.GetPosition(1) - line.GetPosition(0)).magnitude;
    }
    void OnMouseDown()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        DeleteLine();
    }
}