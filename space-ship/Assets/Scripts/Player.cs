using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Ship parameters")]
  [SerializeField] private float shipSpeed = 10f; // Velocidade do movimento lateral e vertical
  [SerializeField] private float bulletSpeed = 8f;

  [Header("Object references")]
  [SerializeField] private Transform bulletSpawn;
  [SerializeField] private Rigidbody2D bulletPrefab;
  [SerializeField] private ParticleSystem destroyedParticles;

  private Rigidbody2D shipRigidbody;
  private bool isAlive = true;

  private void Start()
  {
    shipRigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (isAlive)
    {
      HandleShipMovement();
      HandleShooting();
    }
  }

  private void HandleShipMovement()
  {
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    Vector2 movement = new Vector2(horizontalInput, verticalInput) * shipSpeed * Time.deltaTime;

    shipRigidbody.MovePosition(shipRigidbody.position + movement);
  }

  private void HandleShooting()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

      Vector2 bulletDirection = transform.up;

      bullet.linearVelocity = bulletDirection * bulletSpeed;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Asteroid"))
    {
      isAlive = false;

      GameManager gameManager = FindObjectOfType<GameManager>();
      gameManager.GameOver();

      Instantiate(destroyedParticles, transform.position, Quaternion.identity);

      Destroy(gameObject);
    }
  }
}
