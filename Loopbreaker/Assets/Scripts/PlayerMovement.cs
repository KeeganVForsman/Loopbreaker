using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 movementInput;
    public Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

        // Update animation parameters
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    private void Update()
    {
        // damage test
        if (Input.GetKeyDown(KeyCode.H))
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject.Find("Smo").GetComponent<BossHealthBar>().TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("Ornn").GetComponent<BossHealthBar>().TakeDamage(20);
        }

        // Move using input directly (X/Y only)
        Vector3 moveDirection = new Vector3(movementInput.x, movementInput.y, 0f);

        // Apply movement
        rb.velocity = moveDirection * moveSpeed;

        // Rotate to face movement direction
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
            transform.rotation = targetRotation;
        }
    }
}