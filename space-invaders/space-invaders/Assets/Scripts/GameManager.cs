using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives = 3; // Quantidade inicial de vidas
    public int score = 0; // Pontuação inicial

    public Image[] hearts;
    public TextMeshProUGUI scoreText; // Referência ao texto de Score na UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if(scoreText == null){
            scoreText = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        UpdateUI(); // Atualiza a UI no início do jogo
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;

            // Desativa a imagem do coração correspondente
            if (lives >= 0 && hearts[lives] != null)
            {
                hearts[lives].enabled = false; // Desativa a imagem do coração
            }

            if (lives <= 0)
            {
                SceneManager.LoadScene(2); //Redireciona para a cena de Derrota
            }
            else
            {
                UpdateUI();
            }
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
