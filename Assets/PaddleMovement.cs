using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 9.0f;
    private float paddleHeight;

    void Start()
    {
        paddleHeight = GetComponent<Renderer>().bounds.extents.y;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        // Move Paddle 1
        if (gameObject.name == "Paddle1")
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.up * speed * dt;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.down * speed * dt;
            }
        }

        // Move Paddle 2
        if (gameObject.name == "Paddle2")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += Vector3.up * speed * dt;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += Vector3.down * speed * dt;
            }
        }

        ClampPaddlePosition();
    }

    void ClampPaddlePosition()
    {
        float minY = -4.5f + paddleHeight;
        float maxY = 4.5f - paddleHeight;
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }
}
