using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 deadZoneSize = new Vector2(2f, 1.5f);
    public float smoothSpeed = 5f;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Project target position into screen plane (camera space)
        Vector3 cameraRight = cam.transform.right;
        Vector3 cameraUp = Vector3.Cross(cameraRight, Vector3.forward).normalized;

        Vector3 offset = target.position - transform.position;

        float xOffset = Vector3.Dot(offset, cameraRight);
        float yOffset = Vector3.Dot(offset, cameraUp);

        Vector3 desiredPosition = transform.position;

        if (Mathf.Abs(xOffset) > deadZoneSize.x)
            desiredPosition += cameraRight * (xOffset - Mathf.Sign(xOffset) * deadZoneSize.x);

        if (Mathf.Abs(yOffset) > deadZoneSize.y)
            desiredPosition += cameraUp * (yOffset - Mathf.Sign(yOffset) * deadZoneSize.y);

        // Smoothly move camera
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / smoothSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        if (!Camera.main) return;

        Vector3 center = transform.position;
        Vector3 right = Camera.main.transform.right * deadZoneSize.x;
        Vector3 up = Vector3.Cross(right, Vector3.forward).normalized * deadZoneSize.y;

        Vector3 topLeft = center - right + up;
        Vector3 topRight = center + right + up;
        Vector3 bottomLeft = center - right - up;
        Vector3 bottomRight = center + right - up;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}