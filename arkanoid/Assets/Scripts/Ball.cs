using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 initialVelocity;
    private Rigidbody2D ballRb;
    private bool isBallMoving;
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBallMoving)
        {
            transform.parent = null;
            ballRb.linearVelocity = initialVelocity;
            isBallMoving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blocks")
        {
            Destroy(collision.gameObject);
            GameManager.Instance.BlockDestroyed();
        }
    }

    private void FixVelocidade(){
        float velocidadeDelta = 0.5f;
        float velocidadeMin = 0.2f;

        if(Mathf.Abs(ballRb.linearVelocity.x) < velocidadeMin){
            velocidadeDelta = Random.value < 0.5f ? velocidadeDelta : -velocidadeDelta;
            ballRb.linearVelocity += new Vector2(velocidadeDelta, 0f);
        }
        if(Mathf.Abs(ballRb.linearVelocity.y) < velocidadeMin)
        {
            velocidadeDelta = Random.value < 0.5f ? velocidadeDelta : -velocidadeDelta;
            ballRb.linearVelocity += new Vector2(velocidadeDelta, 0f);
        }
    }

}
