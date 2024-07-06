using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float movementThreshold = 0.01f;
    public Vector3 lastPlayerPosition;
    public bool isPlayerMoving = false;

    void Start()
    {
        lastPlayerPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 deltaPosition = currentPosition - lastPlayerPosition;
        if (deltaPosition.sqrMagnitude > movementThreshold)
        {
            isPlayerMoving = true;
        }
        else
        {
            isPlayerMoving = false;
        }
        lastPlayerPosition = currentPosition;
    }

}
