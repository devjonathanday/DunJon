using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCreator : MonoBehaviour
{
    public GameObject creatorPanel;
    public GameObject helpPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            creatorPanel.SetActive(true);
        if (Input.GetKeyUp(KeyCode.LeftControl))
            creatorPanel.SetActive(false);

        if (Input.GetKeyDown(KeyCode.H))
            helpPanel.SetActive(true);
        if (Input.GetKeyUp(KeyCode.H))
            helpPanel.SetActive(false);
    }
}

//public float spawnRadius;
//public GameObject[] nodePrefabs;
//GameObject[] nodeCopies;

//public Camera cam;
//public bool hidden;

//void Start()
//{
//    nodeCopies = new GameObject[nodePrefabs.Length];
//    for (int i = 0; i < nodePrefabs.Length; i++)
//    {
//        GameObject newNode = Instantiate(nodePrefabs[i]);
//        newNode.transform.SetParent(transform);
//        newNode.SetActive(false);
//        nodeCopies[i] = newNode;
//    }
//    cam = Camera.main;
//}

//void Update()
//{
//    if(Input.GetMouseButtonDown(1))
//    {
//        RevealNodes();
//    }
//    if (Input.GetMouseButtonUp(1))
//    {
//        HideNodes();
//    }
//    if(hidden)
//    {
//        transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
//    }
//}

//void RevealNodes()
//{
//    for(int i = 0; i < nodeCopies.Length; i++)
//    {
//        float angle = i * Mathf.PI * 2 / nodeCopies.Length;
//        Vector3 spawnPos = new Vector3(Mathf.Cos(angle) * spawnRadius, Mathf.Sin(angle) * spawnRadius);
//        nodeCopies[i].transform.position = transform.position + spawnPos;
//        nodeCopies[i].SetActive(true);
//    }
//    hidden = false;
//}
//void HideNodes()
//{
//    for (int i = 0; i < nodeCopies.Length; i++)
//        nodeCopies[i].SetActive(false);

//    hidden = true;
//}