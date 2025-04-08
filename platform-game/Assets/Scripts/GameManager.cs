using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance;
    public TMP_Text scoreText;
    public TMP_Text scoreTextCoin;
    public TMP_Text scoreTextMushroom;
    public int score = 0; 
    public int scoreCoin = 0;
    public int scoreMushroom = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém entre as cenas
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Procura novamente o texto de score na nova cena
        scoreTextCoin = GameObject.FindWithTag("ScoreMoeda")?.GetComponent<TextMeshProUGUI>();
        if (scoreTextCoin != null)
        {
            scoreTextCoin.text = "X " + scoreCoin;
        }

        scoreText = GameObject.FindWithTag("Score")?.GetComponent<TextMeshProUGUI>();
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        scoreTextMushroom = GameObject.FindWithTag("ScoreMushroom")?.GetComponent<TextMeshProUGUI>();
        if(scoreTextMushroom != null)
        {
            scoreTextMushroom.text = "X " + scoreMushroom;
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

        if (scoreTextCoin != null)
            scoreTextCoin.text = "X " + scoreCoin;
    }

    public void AddScoreMushroom(int points)
    {
        scoreMushroom += points;
        Debug.Log("Score atual: " + scoreMushroom);

        if (scoreTextMushroom != null)
            scoreTextMushroom.text = "X " + scoreMushroom;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Fase2");
    }

    public void ResetScores()
    {
        score = 0;
        scoreCoin = 0;
        scoreMushroom = 0;

        // Atualiza a UI se já estiver presente
        if (scoreText != null)
            scoreText.text = "Score: 0";

        if (scoreTextCoin != null)
            scoreTextCoin.text = "X 0";

        if (scoreTextMushroom != null)
            scoreTextMushroom.text = "X 0";
    }
}
