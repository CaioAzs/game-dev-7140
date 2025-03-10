using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float limites = 4.7f;

    void Update()
    {
        Move();
    }
    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 playerPosition = transform.position;

        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * moveSpeed * Time.deltaTime, -limites, limites);
        transform.position = playerPosition;
    }
}
