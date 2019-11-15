using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCreator : MonoBehaviour
{
    public float spawnRadius;
    public GameObject[] nodePrefabs;
    GameObject[] nodeCopies;

    void Start()
    {
        nodeCopies = new GameObject[nodePrefabs.Length];
        for (int i = 0; i < nodePrefabs.Length; i++)
        {
            GameObject newNode = Instantiate(nodePrefabs[i]);
            newNode.SetActive(false);
            nodeCopies[i] = newNode;
        }
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RevealNodes();
        }
        if (Input.GetMouseButtonUp(1))
        {
            HideNodes();
        }
    }

    void RevealNodes()
    {
        for(int i = 0; i < nodeCopies.Length; i++)
        {
            float angle = i * Mathf.PI * 2 / nodeCopies.Length;
            Vector2 spawnPos = new Vector2(Mathf.Cos(angle) * spawnRadius, Mathf.Sin(angle) * spawnRadius);
            nodeCopies[i].transform.position = spawnPos;
            nodeCopies[i].SetActive(true);
        }
    }
    void HideNodes()
    {
        for (int i = 0; i < nodeCopies.Length; i++)
            nodeCopies[i].SetActive(false);
    }
}