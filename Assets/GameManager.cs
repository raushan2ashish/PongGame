using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Ball ball; // Assign the Ball object here
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    private int player1Score;
    private int player2Score;

    private void Awake()
    {
        Instance = this; // Set singleton instance
    }

    void Update()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    public void PlayerScored(int player)
    {
        if (player == 1)
        {
            player1Score++;
        }
        else if (player == 2)
        {
            player2Score++;
        }

        ball.ResetBall(); // Reset the ball after scoring
    }
}
