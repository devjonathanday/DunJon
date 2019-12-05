using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool dragging { get; set; }
    Vector3 mouseDragStart;
    Camera cam;
    void Start()
    {
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
            PointerDown();
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            PointerUp();
    }
    public void PointerDown()
    {
        dragging = true;
    }
    public void PointerUp()
    {
        dragging = false;
    }
}