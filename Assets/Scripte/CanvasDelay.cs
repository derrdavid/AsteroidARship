using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDelay : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField][Range(0, 1)] private float amount = .3f;

    private Quaternion targetRotation;
    private Vector3 targetPosition;

    private void Start()
    {
        targetRotation = mainCamera.transform.rotation;
        targetPosition = mainCamera.transform.position;
    }

    private void Update()
    {
        Quaternion cameraRotation = mainCamera.transform.rotation;
        Quaternion deltaRotation = cameraRotation * Quaternion.Inverse(targetRotation);

        targetRotation = Quaternion.Slerp(targetRotation, cameraRotation, amount);
        targetPosition += deltaRotation * (mainCamera.transform.position - targetPosition);

        transform.rotation = targetRotation;
        transform.position = targetPosition;
    }
}
