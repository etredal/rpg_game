using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    public float movementSpeed = 0f;
    public GameObject checkpoints;
    private List<Vector2> checkpointLocations;
    private int currentCheckpoint = 0;

    private Rigidbody2D rb;

    void Start()
    {
        checkpointLocations = new List<Vector2>();
        rb = GetComponent<Rigidbody2D>();

        for (int n = 0; n < checkpoints.transform.childCount; n++) {
            checkpointLocations.Add(checkpoints.transform.GetChild(n).position);
        }

        Destroy(checkpoints);
    }

    void Update()
    {
        Vector2 moveVelocity = checkpointLocations[currentCheckpoint] - (Vector2) transform.position;
        rb.velocity = moveVelocity.normalized * movementSpeed;

        // Reached checkpoint
        if (Vector2.Distance(transform.position, checkpointLocations[currentCheckpoint]) < 0.1f)
        {
            currentCheckpoint = (currentCheckpoint + 1) % checkpointLocations.Count;
        }
    }
}
