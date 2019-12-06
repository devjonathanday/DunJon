using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineConnector : MonoBehaviour
{
    public LineRenderer line;

    public NodeOutput start;
    public NodeInput end;

    public bool finished;
    public CapsuleCollider clickCollider;

    Camera cam;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        clickCollider = GetComponent<CapsuleCollider>();
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
            if (start != null && end != null)
            {
                line.SetPosition(0, start.transform.position);
                line.SetPosition(1, end.transform.position);
            }
            else DeleteLine();
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

        start.lineReferences.Remove(this);
        Destroy(gameObject);
    }
    void UpdateLineCollider()
    {
        transform.position = (line.GetPosition(0) + line.GetPosition(1)) / 2;
        Vector3 lineVector = line.GetPosition(1) - line.GetPosition(0);
        transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(lineVector.y, lineVector.x), Vector3.forward);
        clickCollider.height = (line.GetPosition(1) - line.GetPosition(0)).magnitude;
    }
    void OnMouseDown()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
        DeleteLine();
    }
    public void Refresh()
    {
        UpdateLineCollider();
    }
}