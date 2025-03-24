using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private float bulletLifetime = 5f; // Tempo maior para testar

    private void Start() {
        Destroy(gameObject, bulletLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) // Evita que a bala colida com a própria nave
        {
            Debug.Log("Bala colidiu com: " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
