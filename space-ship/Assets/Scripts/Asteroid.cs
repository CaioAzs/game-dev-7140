using UnityEngine;

public class Asteroid : MonoBehaviour {
  [SerializeField] private GameObject destroyedAnimationPrefab;
  public GameManager gameManager;

  private void Start() {

    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    float spawnSpeed = Random.Range(3f, 5f) * gameManager.asteroidSpeedMultiplier; 
    rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), -1f).normalized * spawnSpeed; 

    gameManager.asteroidCount++;
  }

  private void Update() {
    if (!GetComponent<Renderer>().isVisible) {
      Destroy(gameObject);  // Destroy when out of screen bounds
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Bullet")) {
        gameManager.asteroidCount--;
        Destroy(collision.gameObject);
        Instantiate(destroyedAnimationPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        gameManager.AddScore(100);
    } 
    else if (collision.CompareTag("BaseInferior")) { // Se tocar na base
        gameManager.SubtractScore(200); // Remove pontos
        Destroy(gameObject); // Remove o asteroide
    }
  }
}
