using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Projectile laserPrefab;
    public float speed = 5.0f;

    private bool _laserActive;

    private float _minX, _maxX;

    private void Start()
    {
        // Define os limites baseado no tamanho da tela
        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x; 
        _minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + halfWidth;
        _maxX = Camera.main.ViewportToWorldPoint(Vector3.one).x - halfWidth;
    }

    private void Update(){
        float move = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            move = -speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            move = speed * Time.deltaTime;
        }

        // Move o jogador e restringe sua posição
        Vector3 newPosition = transform.position + Vector3.right * move;
        newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    private void Shoot(){
        if(!_laserActive){
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed(){
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") || other.gameObject.layer == LayerMask.NameToLayer("Missile")){
            GameManager.Instance.LoseLife();
        }
    }
}
