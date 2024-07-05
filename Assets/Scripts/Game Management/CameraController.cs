using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reconoce si la camara se mueve o no.
public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementThreshold = 0.01f;
    public Vector3 lastPosition;
    public bool isMoving = false;

    void Start()
    {
        lastPosition = Input.acceleration;
    }

    void Update()
    {
        Vector3 deltaPosition = Input.acceleration - lastPosition;
        if (deltaPosition.sqrMagnitude > movementThreshold)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = Input.acceleration;
    }

}
