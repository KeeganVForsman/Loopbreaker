using UnityEngine;

public class SmartCameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Camera Follow Settings")]
    public float pitchAngle = 45f; // X rotation
    public float smoothSpeed = 5f;

    [Header("Dead Zone Settings")]
    public Vector2 deadZoneSize = new Vector2(2f, 1.5f);

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (!target) return;

        // Get camera's forward, right, and up based on pitch
        Quaternion cameraRotation = Quaternion.Euler(pitchAngle, 0f, 0f);
        Vector3 cameraForward = cameraRotation * Vector3.forward;
        Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraForward).normalized;
        Vector3 cameraUp = Vector3.Cross(cameraForward, cameraRight).normalized;

        Vector3 offset = target.position - transform.position;

        float xOffset = Vector3.Dot(offset, cameraRight);
        float yOffset = Vector3.Dot(offset, cameraUp);

        Vector3 desiredPosition = transform.position;

        if (Mathf.Abs(xOffset) > deadZoneSize.x)
            desiredPosition += cameraRight * (xOffset - Mathf.Sign(xOffset) * deadZoneSize.x);

        if (Mathf.Abs(yOffset) > deadZoneSize.y)
            desiredPosition += cameraUp * (yOffset - Mathf.Sign(yOffset) * deadZoneSize.y);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / smoothSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        if (!target) return;

        Quaternion cameraRotation = Quaternion.Euler(pitchAngle, 0f, 0f);
        Vector3 cameraForward = cameraRotation * Vector3.forward;
        Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraForward).normalized;
        Vector3 cameraUp = Vector3.Cross(cameraForward, cameraRight).normalized;

        Vector3 center = transform.position;

        Vector3 topLeft = center - cameraRight * deadZoneSize.x + cameraUp * deadZoneSize.y;
        Vector3 topRight = center + cameraRight * deadZoneSize.x + cameraUp * deadZoneSize.y;
        Vector3 bottomLeft = center - cameraRight * deadZoneSize.x - cameraUp * deadZoneSize.y;
        Vector3 bottomRight = center + cameraRight * deadZoneSize.x - cameraUp * deadZoneSize.y;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}