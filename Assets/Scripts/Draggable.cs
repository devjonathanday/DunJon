using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : Button, IPointerDownHandler, IPointerUpHandler
{
    public bool dragging { get; set; }
    Vector3 mouseDragStart;
    Camera cam;
    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
    }
    
    void Update()
    {
        if (!dragging) { mouseDragStart = cam.ScreenToWorldPoint(Input.mousePosition); }

        if(dragging)
        {
            Vector2 difference = cam.ScreenToWorldPoint(Input.mousePosition) - mouseDragStart;
            transform.Translate(difference.x, difference.y, 0);
            mouseDragStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerDown(eventData);
            dragging = true;
        }
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(eventData);
            dragging = false;
        }
    }
}