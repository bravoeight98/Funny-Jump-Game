using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 10f;
    private BoxCollider2D groundCheckCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D
    }

    void Update()
    {
        // Movement (unchanged)
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Function to check if grounded using the BoxCollider2D
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckCollider.bounds.center,
                                   groundCheckCollider.bounds.size,
                                   0f, LayerMask.GetMask("Ground"));
    }
}
