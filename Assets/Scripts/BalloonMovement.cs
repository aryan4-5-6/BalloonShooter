using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BalloonMovement : MonoBehaviour
{
    private float speed = 4f;
    private Vector2 direction;
    private bool isZigZag;
    private float zigZagAmplitude = 5f;
    private float waveFrequency = 3f;
    private float time;
    private Rigidbody2D rb;

    public void InitializeMovement(Transform spawnPoint)
    {
        int pattern = Random.Range(0, 2);
        isZigZag = pattern == 1;

        if (spawnPoint.name == "BottomSpawn")
        {
            direction = Vector2.up;
        }
        else if (spawnPoint.name == "LeftSpawn")
        {
            direction = Vector2.right;
        }
        else if (spawnPoint.name == "RightSpawn")
        {
            direction = Vector2.left;
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        Vector2 movement = direction * speed;

        if (isZigZag)
        {
            Vector2 perpendicularDirection = Vector2.Perpendicular(direction).normalized;
            movement += perpendicularDirection * Mathf.Sin(time * waveFrequency) * zigZagAmplitude;
        }

        rb.velocity = movement;
    }
}
