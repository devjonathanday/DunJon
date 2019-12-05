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
        }
    }

    public void StartLine(NodeOutput startObject)
    {
        finished = false;
        start = startObject;
    }
    public void FinishLine(NodeInput endObject)
    {
        finished = true;
        end = endObject;
        end.value = start.value;
    }
}