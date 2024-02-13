using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    //public float speed = 5f;
    public float jumpForce = 5f;
    private BoxCollider2D groundCheckCollider;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Movement (unchanged)
        //float horizontal = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Jumping
        if (Input.GetButtonDown("Fire") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            ChangeColors();
        }
    }

    // Function to check if grounded using the BoxCollider2D
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckCollider.bounds.center,
                                   groundCheckCollider.bounds.size,
                                   0f, LayerMask.GetMask("Ground"));
    }

    void ChangeColors()
    {
        Color newPlayerColor;
        Color newBackgroundColor;

        // Generate distinct random colors
        do
        {
            newPlayerColor = Random.ColorHSV();
            newBackgroundColor = Random.ColorHSV();
        } while (CalculateColorDifference(newPlayerColor, newBackgroundColor) < 0.5f);

        spriteRenderer.color = newPlayerColor;
        mainCamera.backgroundColor = newBackgroundColor;
    }

    // Custom function to calculate color difference based on Euclidean distance in RGB space
    float CalculateColorDifference(Color color1, Color color2)
    {
        float difference = 0f;
        difference += Mathf.Pow(color1.r - color2.r, 2f);
        difference += Mathf.Pow(color1.g - color2.g, 2f);
        difference += Mathf.Pow(color1.b - color2.b, 2f);
        return Mathf.Sqrt(difference);
    }
}
