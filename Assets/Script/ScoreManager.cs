using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [Header("引用")]
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI bestScoreText;
    
    private int currentScore = 0;
    private int bestScore = 0;

    void Start()
    {
        LoadBestScore();
        UpdateScoreUI();
    }

    public void AddScore()
    {
        currentScore++;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    public void SaveBestScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
        UpdateScoreUI();
    }

    void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void UpdateScoreUI()
    {
        currentScoreText.text = currentScore.ToString();
        finalScoreText.text = currentScore.ToString();
        bestScoreText.text = bestScore.ToString();
    }
}
