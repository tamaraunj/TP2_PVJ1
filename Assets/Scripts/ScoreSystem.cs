using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TMP_Text scoreText;
    public int Score => score;
    private void Start()
    {
        UpdateScoreText();
    }
    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
        //Debug.Log("Puntuación actual: " + score);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.NotifyPlayerStatusChanged();
        }
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
    }
}
