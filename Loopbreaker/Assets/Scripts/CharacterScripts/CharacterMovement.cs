using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 input;
    private Vector2 isRight = new Vector2(-1f, 0.5f);
    private Vector2 isUP = new Vector2(-1f, -0.5f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        input = new Vector2(horizontal, vertical);
    }
    private void FixedUpdate()
    {
        Vector2 moveDirection = (isRight * input.x + isUP * input.y).normalized;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }
}
