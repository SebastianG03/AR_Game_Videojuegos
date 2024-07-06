using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    private PetAnimator petAnimator;
    private bool isPlayerMoving = false;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        petAnimator = GetComponent<PetAnimator>();
    }

    void Update()
    {
        isPlayerMoving = cameraController.isPlayerMoving;
        lastPlayerPosition = cameraController.lastPlayerPosition;
    }
}
