using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private SpriteRenderer sr;
    float moveSpeed = 6;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);

        if (horizontalInput > 0) {
            sr.flipX = false;
        } else if (horizontalInput < 0) {
            sr.flipX = true;
        }
    }
}
