using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private SpriteRenderer sr;
    float moveSpeed = 0.8f;
    private Rigidbody2D body;
    private Animator animator;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        body.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);

        // Sprite Renderer Logic
        if (horizontalInput > 0) {
            sr.flipX = false;
        } else if (horizontalInput < 0) {
            sr.flipX = true;
        }

        // Animation Logic
        if (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput) != 0) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }
}
