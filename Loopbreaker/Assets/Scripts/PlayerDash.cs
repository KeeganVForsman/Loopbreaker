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

    [Header("Dash Effects")]
    public GameObject playerVisuals; // Drag the visual part of the player here
    public Collider hurtbox; // Assign the collider that takes damage

    private Vector2 moveInput;
    private Vector3 dashDirection;
    private bool isDashing = false;
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

        RaycastHit hit;
        bool hitWall = Physics.Raycast(
            transform.position,
            dashDirection,
            out hit,
            maxDistance,
            dashObstacleMask
        );

        float finalDistance = hitWall ? hit.distance - 0.05f : maxDistance;
        dashTarget = transform.position + dashDirection * finalDistance;

        isDashing = true;
        cooldownTime = dashCooldown;

        if (dashTrail) dashTrail.emitting = true;
        if (playerVisuals) playerVisuals.SetActive(false); // Turn invisible
        if (hurtbox) hurtbox.enabled = false; // Give i-frames
    }

    private void EndDash()
    {
        isDashing = false;

        if (dashTrail) dashTrail.emitting = false;
        if (playerVisuals) playerVisuals.SetActive(true); // Make visible again
        if (hurtbox) hurtbox.enabled = true; // Remove i-frames
    }
}