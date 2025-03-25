using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  [SerializeField] private Asteroid asteroidPrefab;

  public int asteroidCount = 0;
  private int score = 0;

  [Header("Asteroid spawn parameters")]
  [SerializeField] private float spawnInterval = 2f;
  private float spawnSpeedIncrement = 0.1f;

  [Header("UI References")]
  [SerializeField] private TextMeshProUGUI scoreText;
  public float asteroidSpeedMultiplier = 1f;
  private int nextSlowdownScore = 3000; // Começa em 1000 e aumenta a cada ativação


  private void Start()
  {
    StartCoroutine(SpawnAsteroidAtInterval());
    UpdateScoreText();
  }

  private void Update()
  {
  }

  private IEnumerator SpawnAsteroidAtInterval()
  {
    while (true)
    {
      SpawnAsteroid();

      spawnInterval = Mathf.Max(0.5f, spawnInterval - spawnSpeedIncrement);

      yield return new WaitForSeconds(spawnInterval);
    }
  }

  private void SpawnAsteroid()
  {
    int numAsteroids = Random.Range(1, 4);

    for (int i = 0; i < numAsteroids; i++)
    {
      float offset = Random.Range(0f, 1f);
      Vector2 viewportSpawnPosition = new Vector2(offset, 1);
      Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
      Asteroid asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
      asteroid.gameManager = this;
    }
  }

  public void AddScore(int points)
  {
    score += points;
    UpdateScoreText();

    if (score >= nextSlowdownScore)
    {
      asteroidSpeedMultiplier = 0.5f; // Reduz a velocidade dos asteroides
      Debug.Log("DIMINUINDO VELOCIDADE DOS ASTEROIDS...");

      nextSlowdownScore += 3000; // Atualiza para o próximo gatilho (2000, 4000, etc.)
      StartCoroutine(ResetAsteroidSpeed()); // Restaura a velocidade após 5s
    }

    if (score >= 8000)
    {
      Debug.Log("8000 pontos atingidos...");
      LoadNextLevel();
    }
  }

  private void LoadNextLevel()
  {
    Debug.Log("Carregando tela de vitória...");
    SceneManager.LoadScene(1);
  }
  private void UpdateScoreText()
  {
    scoreText.text = "Score: " + score;
  }

  public void GameOver()
  {
    StartCoroutine(Restart());
  }

  private IEnumerator ResetAsteroidSpeed()
  {
    yield return new WaitForSeconds(5f);
    asteroidSpeedMultiplier = 1f; // Volta à velocidade normal
  }

  private IEnumerator Restart()
  {
    Debug.Log("Restarting game in 2 seconds...");
    yield return new WaitForSeconds(2f);

    Debug.Log("Loading scene 2...");
    SceneManager.LoadScene(2);

    yield return null;
  }


  public void SubtractScore(int points)
  {
    score -= points;

    if (score <= 0)
    {
      RestartedByScore(); // Reinicia o jogo imediatamente
    }
    else
    {
      UpdateScoreText(); // Só atualiza se o jogador ainda estiver no jogo
    }
  }

  public void RestartedByScore()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

}
