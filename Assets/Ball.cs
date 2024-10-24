using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 direction;
    public float speed = 5.0f;
    // Reference to the paddles
    public GameObject paddle1;
    public GameObject paddle2;

    // Paddle size (assuming both paddles have the same size)
    private float paddleWidth;
    private float paddleHeight;

    // Ball size (assuming it's a circle)
    private float ballRadius;

    void Start()
    {
        // Start the ball moving in a random direction
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Get the paddle size from its BoxCollider2D
        paddleWidth = paddle1.GetComponent<BoxCollider2D>().size.x * paddle1.transform.localScale.x;
        paddleHeight = paddle1.GetComponent<BoxCollider2D>().size.y * paddle1.transform.localScale.y;

        // Get the ball size from its CircleCollider2D (assuming it's a circle)
        ballRadius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
    }

    void Update()
    {
        MoveBall();
        CheckBounds();
        CheckPaddleCollision(); // Manually check for paddle collisions
    }

    void MoveBall()
    {
        float dt = Time.deltaTime;
        transform.position += (Vector3)(direction * speed * dt);
    }

    void CheckBounds()
    {
        // If the ball hits the top or bottom wall
        if (transform.position.y > 4.5f - ballRadius || transform.position.y < -4.5f + ballRadius)
        {
            direction.y = -direction.y; // Reverse Y direction
        }

        // If the ball goes out of bounds (right/left), reset it
        if (transform.position.x > 9.5f + ballRadius)
        {
            GameManager.Instance.PlayerScored(1); // Player 1 scores
            ResetBall();
        }
        else if (transform.position.x < -9.5f - ballRadius)
        {
            GameManager.Instance.PlayerScored(2); // Player 2 scores
            ResetBall();
        }
    }

    void CheckPaddleCollision()
    {
        // Paddle 1 collision
        if (IsCollidingWithPaddle(paddle1))
        {
            Hit(); // Reverse the ball's direction
        }

        // Paddle 2 collision
        if (IsCollidingWithPaddle(paddle2))
        {
            Hit(); // Reverse the ball's direction
        }
    }

    bool IsCollidingWithPaddle(GameObject paddle)
    {
        // Get the paddle's position
        Vector2 paddlePos = paddle.transform.position;

        // Get the ball's position
        Vector2 ballPos = transform.position;

        // Check if the ball is within the paddle's X range
        bool withinX = (ballPos.x - ballRadius < paddlePos.x + paddleWidth / 2) && (ballPos.x + ballRadius > paddlePos.x - paddleWidth / 2);

        // Check if the ball is within the paddle's Y range
        bool withinY = (ballPos.y - ballRadius < paddlePos.y + paddleHeight / 2) && (ballPos.y + ballRadius > paddlePos.y - paddleHeight / 2);

        // If both X and Y conditions are true, then the ball is colliding with the paddle
        return withinX && withinY;
    }

    public void Hit()
    {
        direction.x = -direction.x; // Reverse X direction when hitting a paddle
        float angle = Random.Range(-15f, 15f); // Randomize the angle slightly
        direction = Quaternion.Euler(0, 0, angle) * direction; // Adjust the direction slightly
    }

    public void ResetBall()
    {
        // Reset position and set a new random direction
        transform.position = Vector3.zero;
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}