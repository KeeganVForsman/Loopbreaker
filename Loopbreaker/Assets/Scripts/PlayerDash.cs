using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;
    public TrailRenderer dashTrail;

    private Vector2 moveInput;
    private Vector3 dashDirection;
    private bool isDashing = false;
    private float dashTime;
    private float cooldownTime;

    private void Update()
    {
        // Cooldown timer
        if (cooldownTime > 0)
            cooldownTime -= Time.deltaTime;

        // Dash logic
        if (isDashing)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            dashTime -= Time.deltaTime;

            if (dashTime <= 0f)
            {
                isDashing = false;
                if (dashTrail) dashTrail.emitting = false;
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

        if (inputDir != Vector3.zero)
        {
            dashDirection = inputDir.normalized;
            isDashing = true;
            dashTime = dashDuration;
            cooldownTime = dashCooldown;

            if (dashTrail) dashTrail.emitting = true;

            // Optional: trigger dash animation, sound, etc.
        }
    }
}