using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach directly to Camera object!
public class ViewControls : MonoBehaviour
{
    public Camera cam;
    public Vector2 orthoSizeRange;
    [Range(0, 1)] public float zoomSpeed;

    Vector3 mouseDragStart;
    
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize -= cam.orthographicSize * zoomSpeed, orthoSizeRange.x, orthoSizeRange.y);
        if (Input.mouseScrollDelta.y < 0)
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize += cam.orthographicSize * zoomSpeed, orthoSizeRange.x, orthoSizeRange.y);
        if (!Input.GetMouseButton(2))
        {
            mouseDragStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(2))
        {
            Vector2 difference = mouseDragStart - cam.ScreenToWorldPoint(Input.mousePosition);
            transform.Translate(difference.x, difference.y, 0);
            mouseDragStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}