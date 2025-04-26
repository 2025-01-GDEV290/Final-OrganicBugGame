using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 6f;
    public float jumplock = 0.01f;
    private Vector3 flip;

    private Rigidbody2D rb;
    private Vector2 movement;

    public GameObject spawn;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.position = spawn.transform.position;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        // Basic jump check � jump only when vertical velocity is near zero
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < jumplock)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(rb.position.y < -10)
        {
            rb.position = spawn.transform.position; //new Vector2(-6, 11);
        }

        if (rb.velocity.x > 0)
        {
            flip = transform.localScale;
            flip.x = -1;
            transform.localScale = flip;
        }
        if (rb.velocity.x < 0)
        {
            flip = transform.localScale;
            flip.x = 1;
            transform.localScale = flip;
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}
