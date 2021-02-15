using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private SpriteRenderer sr;
    float moveSpeed = 0.8f;
    private Rigidbody2D body;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        body.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);

        if (horizontalInput > 0) {
            sr.flipX = false;
        } else if (horizontalInput < 0) {
            sr.flipX = true;
        }
    }
}
