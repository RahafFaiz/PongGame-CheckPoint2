using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int player1Score;
    private int player2Score;
    public GameObject Ball;
    public Button resumeButton;
    public Button resetButton; // زر إعادة تعيين السكور

    [SerializeField] private TMP_Text player1Text;
    [SerializeField] private TMP_Text player2Text;

    private bool canScore = true; // منع تسجيل أكثر من نقطة لكل هدف

    void Start()
    {
        //  إعادة تعيين السكور عند بدء اللعبة لمنع استرجاع القيم القديمة
        player1Score = 0;
        player2Score = 0;
        PlayerPrefs.SetInt("Player1Score", player1Score);
        PlayerPrefs.SetInt("Player2Score", player2Score);
        PlayerPrefs.Save();

        UpdateScoreUI();

        if (resumeButton != null)
        {
            resumeButton.gameObject.SetActive(false);
        }

        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetScores);
        }
    }

    public void AddScore(string playerName)
    {
        if (!canScore) return; //  منع تسجيل أكثر من نقطة لنفس الهدف
        canScore = false; // تعطيل التسجيل مؤقتًا


        if (playerName == "Player1")
        {
            player1Score++;
            PlayerPrefs.SetInt("Player1Score", player1Score);
        }
        else if (playerName == "Player2")
        {
            player2Score++;
            PlayerPrefs.SetInt("Player2Score", player2Score);
        }

        PlayerPrefs.Save();
        UpdateScoreUI();

        // إعادة تموضع الكرة وتوقف مؤقت للعبة
        Ball.transform.position = new Vector3(10.06f, 7.77f, 0f);
        Time.timeScale = 0;

        // إظهار زر الاستئناف
        if (resumeButton != null)
        {
            resumeButton.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        canScore = true; //  إعادة تمكين تسجيل النقاط عند استئناف اللعب

        if (resumeButton != null)
        {
            resumeButton.gameObject.SetActive(false);
        }
    }

    private void UpdateScoreUI()
    {
        player1Text.SetText($"Player1 Score: {player1Score}");
        player2Text.SetText($"Player2 Score: {player2Score}");
    }

    public void ResetScores()
    {
        PlayerPrefs.DeleteKey("Player1Score");
        PlayerPrefs.DeleteKey("Player2Score");
        PlayerPrefs.Save();

        player1Score = 0;
        player2Score = 0;
        UpdateScoreUI();
    }
}

