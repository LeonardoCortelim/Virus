using UnityEngine;
using TMPro; // <- Importa o TextMesh Pro

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // <- Agora usa TMP_Text
    private float score;
    private bool isPlayerAlive = true;

    void Start()
    {
        // Garante que o score começa do zero
        score = 0;
        UpdateScoreText();
    }

    void Update()
    {
        // Checa se o player ainda existe
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && isPlayerAlive)
        {
            score += 1 * Time.deltaTime;
            UpdateScoreText();
        }
        else
        {
            isPlayerAlive = false;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = ((int)score).ToString();
    }

    public void ResetScore()
    {
        score = 0;
        isPlayerAlive = true;
        UpdateScoreText();
    }

    public int GetFinalScore()
    {
        return (int)score;
    }
}
