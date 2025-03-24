using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;

public class GameManager : MonoBehaviour {
  [SerializeField] private Asteroid asteroidPrefab;
  
  public int asteroidCount = 0;
  private int score = 0;

  [Header("Asteroid spawn parameters")]
  [SerializeField] private float spawnInterval = 2f; 
  private float spawnSpeedIncrement = 0.1f;

  [Header("UI References")]
  [SerializeField] private TextMeshProUGUI scoreText; 
  private void Start() {
    StartCoroutine(SpawnAsteroidAtInterval());
    UpdateScoreText(); 
  }

  private void Update() {
  }

  private IEnumerator SpawnAsteroidAtInterval() {
    while (true) {
      SpawnAsteroid();

      spawnInterval = Mathf.Max(0.5f, spawnInterval - spawnSpeedIncrement);

      yield return new WaitForSeconds(spawnInterval);
    }
  }

  private void SpawnAsteroid() {
    int numAsteroids = Random.Range(1, 4); 

    for (int i = 0; i < numAsteroids; i++) {
      float offset = Random.Range(0f, 1f);
      Vector2 viewportSpawnPosition = new Vector2(offset, 1); 
      Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
      Asteroid asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
      asteroid.gameManager = this;
    }
  }

  public void AddScore(int points) {
    score += points;
    UpdateScoreText(); 

  private void UpdateScoreText() {
    scoreText.text = "Score: " + score;
  }

  public void GameOver() {
    StartCoroutine(Restart());
  }

  private IEnumerator Restart() {
    Debug.Log("Game Over");

    yield return new WaitForSeconds(2f);

    // Restart scene.
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    yield return null;
  }
}
