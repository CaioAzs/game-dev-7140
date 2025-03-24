using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
  [Header("Ship parameters")]
  [SerializeField] private float shipAcceleration = 10f;
  [SerializeField] private float shipMaxVelocity = 10f;
  [SerializeField] private float shipRotationSpeed = 180f;
  [SerializeField] private float bulletSpeed = 8f;

  [Header("Object references")]
  [SerializeField] private Transform bulletSpawn;
  [SerializeField] private Rigidbody2D bulletPrefab;
  [SerializeField] private ParticleSystem destroyedParticles;

  private Rigidbody2D shipRigidbody;
  private bool isAlive = true;
  private bool isAccelerating = false;

  private void Start() {
    shipRigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update() {
    if (isAlive) {
      HandleShipAcceleration();
      HandleShipRotation();
      HandleShooting();
    }
  }

  private void FixedUpdate() {
    if (isAlive && isAccelerating) {
      shipRigidbody.AddForce(shipAcceleration * transform.up);
      shipRigidbody.linearVelocity = Vector2.ClampMagnitude(shipRigidbody.linearVelocity, shipMaxVelocity);
    }
  }

  private void HandleShipAcceleration() {
    isAccelerating = Input.GetKey(KeyCode.W);
  }

  private void HandleShipRotation() {
    if (Input.GetKey(KeyCode.A)) {
      transform.Rotate(shipRotationSpeed * Time.deltaTime * Vector3.forward);
    } else if (Input.GetKey(KeyCode.D)) {
      transform.Rotate(-shipRotationSpeed * Time.deltaTime * Vector3.forward);
    }
  }

  private void HandleShooting() {
    if (Input.GetKeyDown(KeyCode.Space)) {
      Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
      Vector2 shipVelocity = shipRigidbody.linearVelocity;
      Vector2 shipDirection = transform.up;
      float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

      if (shipForwardSpeed < 0) { 
        shipForwardSpeed = 0; 
      }

      bullet.linearVelocity = shipDirection * shipForwardSpeed;
      bullet.AddForce(bulletSpeed * shipDirection, ForceMode2D.Impulse);
    }
  }
}