using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Which Transform the camera should track.")]
    public Transform whatToFollow;

    [Header("Top Down parameters")]
    [Tooltip("Distance of the camera from the target.")]
    public float topDownDistance = 50;
    [Tooltip("Rotation of the camera.")]
    public Vector3 topDownAngle = new Vector3(60, 0, 0);
    [Tooltip("Smoothing of the camera's rotation. The lower the value, the smoother the rotation. Set to 0 to disable smoothing.")]
    public float topDownInterpolation = 10;

    public float movementSpeed = 2.0f;

    private void Start() {

    }

    void FixedUpdate() {
        Vector3 followPosition = whatToFollow.position;
        Vector3 targetPosition = transform.position;
        float deltaTime = Time.fixedDeltaTime;
        Quaternion targetRotation = Quaternion.Euler(topDownAngle);
        targetPosition = Vector3.Lerp(targetPosition, followPosition + targetRotation * Vector3.back * topDownDistance, Mathf.Clamp01(topDownInterpolation <= 0 ? 1 : topDownInterpolation * deltaTime));
        transform.SetPositionAndRotation(
            Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed),
            Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * movementSpeed));
    }

    public void OnTimeout(GameObject winner) {
        if (!winner) { return; }
        whatToFollow = winner.transform;
        topDownDistance = 21;
        topDownAngle.x = 25;
    }
}
