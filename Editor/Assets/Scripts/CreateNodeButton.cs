using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class CreateNodeButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject node;
    public Transform worldCanvas;
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Draggable newNode = Instantiate(node, worldCanvas, true).GetComponent<Draggable>();
        Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
        newNode.transform.position = new Vector3(newPos.x, newPos.y, 0);
        //newNode.PointerDown();
    }
}

//[CustomEditor(typeof(CreateNodeButton))]
//public class CreateNodeButtonEditor : UnityEditor.UI.ButtonEditor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//    }
//}