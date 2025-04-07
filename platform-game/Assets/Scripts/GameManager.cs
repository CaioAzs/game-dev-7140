using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance;
    public TMP_Text scoreText;
    public TMP_Text scoreTextCoin;
    public int score = 0; 
    public int scoreCoin = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém entre as cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score atual: " + score);

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void AddScoreCoins(int points)
    {
        scoreCoin += points;
        Debug.Log("Score atual: " + scoreCoin);

        if (scoreTextCoin != null)
            scoreTextCoin.text = "X " + scoreCoin;
    }

    /*public void LoadNextLevel()
    {
        SceneManager.LoadScene("Fase2");
    }*/
}
