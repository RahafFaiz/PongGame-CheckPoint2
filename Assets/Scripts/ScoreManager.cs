using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int player1Score = 0;
    private int player2Score = 0;

    [SerializeField] private TMP_Text player1Text;
    [SerializeField] private TMP_Text player2Text;

    public void AddScore(string playerName)
    {
        if (playerName == "Player1")
        {
            player1Score++;
            player1Text.SetText($"Player1 score: {player1Score}"); // 
        }
        else if (playerName == "Player2")
        {
            player2Score++;
            player2Text.SetText($"Player2 score: {player2Score}"); // 
        }
    }
}
