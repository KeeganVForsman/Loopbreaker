using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerAnimController : MonoBehaviour
{
    private Animator animator;
    private Vector2 moveInput;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Called by the Player Input component when "Move" action changes
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // Update the Speed parameter in the Animator
        float speed = moveInput.magnitude;
        animator.SetFloat("Speed", speed);
    }
}