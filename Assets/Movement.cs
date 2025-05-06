using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 6f;
    public float jumplock = 0.01f;
    private Vector3 flip;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;

    public GameObject spawn;

    public AudioSource walking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        rb.position = spawn.transform.position;
    }

    void Update()
    {
        if (!enabled) return;

        movement.x = Input.GetAxisRaw("Horizontal");

        if (movement.x == 0) 
        {
            animator.SetInteger("movement", 0);
        }
        else
        {
            animator.SetInteger("movement", 1);
        }

        if (movement.x > 0)
            sr.flipX = true;  // Facing right
        else if (movement.x < 0)
            sr.flipX = false;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < jumplock)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetInteger("movement", 2);
        }

        if (rb.position.y < -10)
        {
            rb.position = spawn.transform.position;
        }

        if (rb.velocity.x > 0)
        {
            flip = transform.localScale;
            flip.x = -1;
            transform.localScale = flip;

            if (!walking.isPlaying)
            {
                walking.Play();
            }
        }
        else if (rb.velocity.x < 0)
        {
            flip = transform.localScale;
            flip.x = 1;
            transform.localScale = flip;

            if (!walking.isPlaying)
            {
                walking.Play();
            }
        }
        else
        {
            walking.Pause();
        }
    }

    void FixedUpdate()
    {
        if (!enabled) return;

        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}
