using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("View Controls")]
    public Vector2 mouseDelta;
    public float lookSpeed;
    public float vLookScalar;
    public Transform cam;

    [Header("Movement")]
    public float moveSpeed;
    public Vector3 inputVector;
    public Rigidbody rBody;
    public float simulatedDrag;

    [Header("Animation")]
    public Animator animator;
    public float maxIdleVelocity;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        #region Physics/Movement

        mouseDelta.x = Input.GetAxis("Mouse X");
        mouseDelta.y = Input.GetAxis("Mouse Y");

        //Rotate the player based on mouse X
        transform.Rotate(transform.up, mouseDelta.x * lookSpeed);

        //Clamping vertical looking using a scalar value
        vLookScalar += (mouseDelta.y / 90) * lookSpeed;
        vLookScalar = Mathf.Clamp(vLookScalar, -1, 1);

        //Apply the rotation
        if (vLookScalar <= 0)
            cam.localEulerAngles = new Vector3(vLookScalar * -90, 0, 0);
        else if (vLookScalar > 0)
            cam.localEulerAngles = new Vector3((vLookScalar * -90) + 360, 0, 0);

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");
        rBody.AddRelativeForce(inputVector.normalized * moveSpeed);

        Vector3 velocity = rBody.velocity;
        velocity.x *= simulatedDrag;
        velocity.z *= simulatedDrag;
        rBody.velocity = velocity;

        if (rBody.velocity.sqrMagnitude > maxIdleVelocity)
            animator.SetBool("Walking", true);
        else animator.SetBool("Walking", false);

        #endregion
    }
}
