using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 10, Input.GetAxis("Vertical") * Time.deltaTime * 10, 0);
    }
}
