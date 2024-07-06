using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PetController : MonoBehaviour
{
    private PetAnimator petAnimator;
    private CharacterController characterController;

    public float moveSpeed = 2.0f;
    public float turnSpeed = 100.0f;

    public ARPlaneManager arPlaneManager;
    public ARPointCloudManager arPointCloudManager;
    public GameObject planePrefab;
    public GameObject pointPrefab;

    private Transform arCameraTransform;
    private CameraController cameraController;
    private List<ARPlane> arPlanes = new List<ARPlane>();

    private void Awake()
    {
        petAnimator = GetComponent<PetAnimator>();
        characterController = GetComponent<CharacterController>();
        arCameraTransform = Camera.main.transform; // Obtener la referencia a la camara AR
        cameraController = arCameraTransform.GetComponent<CameraController>();
    }

    private void Start()
    {
        // Configuracion de AR managers
        if (arPlaneManager == null)
        {
            arPlaneManager = FindObjectOfType<ARPlaneManager>();
        }
        if (arPointCloudManager == null)
        {
            arPointCloudManager = FindObjectOfType<ARPointCloudManager>();
        }

        arPlaneManager.planesChanged += OnPlanesChanged;
        arPointCloudManager.pointCloudsChanged += OnPointCloudsChanged;

        StartCoroutine(SitRoutine());
    }

    private void OnDestroy()
    {
        arPlaneManager.planesChanged -= OnPlanesChanged;
        arPointCloudManager.pointCloudsChanged -= OnPointCloudsChanged;
    }

    private void Update()
    {
        if (cameraController.isPlayerMoving)
        {
            MovePet();
        }
        else
        {
            petAnimator.Sitting(true, false);
        }
    }

    /// <summary>
    /// Mueve al perro en direccion alrededor del centro de la pantalla.
    /// </summary>
    private void MovePet()
    {
        // Movimiento en circulo alrededor del centro de la pantalla
        Vector3 directionToMove = (arCameraTransform.position - transform.position).normalized;
        directionToMove.y = 0; // Mantener el movimiento en el plano XZ

        // Verificar si hay un plano debajo del perro
        if (IsOnPlane())
        {
            petAnimator.WalkingNormal(true);
            Vector3 move = directionToMove * moveSpeed * Time.deltaTime;

            // Evitar obstaculos y mantener en el suelo
            if (CanMoveToPosition(transform.position + move))
            {
                characterController.Move(move);

                Quaternion toRotation = Quaternion.LookRotation(directionToMove, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            }
        }
        else
        {
            petAnimator.WalkingNormal(false);
        }
    }

    /// <summary>
    /// Corutina que hace que el perro se siente cada 3 minutos.
    /// </summary>
    private IEnumerator SitRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(180); // 3 minutos
            petAnimator.Sitting(true, false);
            yield return new WaitForSeconds(5); // Tiempo sentado
            petAnimator.Sitting(false, false);
        }
    }

    /// <summary>
    /// Maneja los cambios en los planos detectados por AR.
    /// </summary>
    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (ARPlane plane in args.added)
        {
            arPlanes.Add(plane);
            Instantiate(planePrefab, plane.transform);
        }
    }

    /// <summary>
    /// Maneja los cambios en las nubes de puntos detectadas por AR.
    /// </summary>
    private void OnPointCloudsChanged(ARPointCloudChangedEventArgs args)
    {
        foreach (ARPointCloud pointCloud in args.added)
        {
            foreach (Vector3 point in pointCloud.positions)
            {
                Instantiate(pointPrefab, point, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// Verifica si el perro esta sobre un plano detectado.
    /// </summary>
    /// <returns>True si el perro esta sobre un plano, de lo contrario false.</returns>
    private bool IsOnPlane()
    {
        foreach (var plane in arPlanes)
        {
            if (plane.boundary.Contains(transform.position))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Verifica si el perro puede moverse a la posicion deseada.
    /// </summary>
    /// <param name="position">La posicion deseada.</param>
    /// <returns>True si el perro puede moverse a la posicion, de lo contrario false.</returns>
    private bool CanMoveToPosition(Vector3 position)
    {
        foreach (var plane in arPlanes)
        {
            if (plane.boundary.Contains(position))
            {
                return true;
            }
        }
        return false;
    }
}
