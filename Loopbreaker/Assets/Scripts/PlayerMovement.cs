using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 movementInput;

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void Update()
    {
        // Move using input directly (X/Y only)
        Vector3 moveDirection = new Vector3(movementInput.x, movementInput.y, 0f);

        // Apply movement
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotate to face movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.15f);
        }
    }
}