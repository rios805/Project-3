using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject scoreAdvanceTable;

    private int currentScore = 0;
    private int highScore = 0;

    private const string HIGH_SCORE_KEY = "HighScore"; 

    void Start()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0); // Load high score
        UpdateUI();
        if (SceneManager.GetActiveScene().name == "Main Game") 
        {
            if (scoreAdvanceTable != null)
            {
                scoreAdvanceTable.SetActive(false);
            }
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {currentScore:D4}"; 
        highScoreText.text = $"Hi-Score: {highScore:D4}";
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }
}
