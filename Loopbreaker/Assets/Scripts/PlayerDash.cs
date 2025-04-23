using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;
    public TrailRenderer dashTrail;
    public LayerMask dashObstacleMask;
    public float playerRadius = 0.3f;

    private Vector2 moveInput;
    private Vector3 dashDirection;
    private bool isDashing = false;
    private float dashTime;
    private float cooldownTime;

    private Vector3 dashTarget;

    private void Update()
    {
        if (cooldownTime > 0)
            cooldownTime -= Time.deltaTime;

        if (isDashing)
        {
            Vector3 direction = (dashTarget - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, dashTarget);
            float moveDistance = dashSpeed * Time.deltaTime;

            if (moveDistance >= distanceToTarget)
            {
                transform.position = dashTarget;
                EndDash();
            }
            else
            {
                transform.position += direction * moveDistance;
            }
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnDash(InputValue value)
    {
        if (cooldownTime > 0 || isDashing) return;

        Vector3 inputDir = new Vector3(moveInput.x, moveInput.y, 0f);
        if (inputDir == Vector3.zero) return;

        dashDirection = inputDir.normalized;
        float maxDistance = dashSpeed * dashDuration;

        // Only block dash with collisions on the 'Wall' layer
        RaycastHit hit;
        bool hitWall = Physics.Raycast(
            transform.position,
            dashDirection,
            out hit,
            maxDistance,
            dashObstacleMask // make sure this only includes the Wall layer!
        );

        float finalDistance = hitWall ? hit.distance - 0.05f : maxDistance;
        dashTarget = transform.position + dashDirection * finalDistance;

        isDashing = true;
        dashTime = dashDuration;
        cooldownTime = dashCooldown;

        if (dashTrail) dashTrail.emitting = true;
    }

    private void EndDash()
    {
        isDashing = false;
        if (dashTrail) dashTrail.emitting = false;
    }
}