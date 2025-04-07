using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Goomba : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody2D rdGoomba;
    [SerializeField] float speed = 2f;
    [SerializeField] Transform point1, point2;
    [SerializeField] LayerMask layer;
    [SerializeField] bool isColliding;

    Animator animGoomba;
    BoxCollider2D colliderGoomba;

    private void Awake(){
        rdGoomba = GetComponent<Rigidbody2D>();
        animGoomba = GetComponent<Animator>();
        colliderGoomba = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    private void FixedUpdate(){
        rdGoomba.linearVelocity = new Vector2(speed, rdGoomba.linearVelocity.y);

        isColliding = Physics2D.Linecast(point1.position, point2.position, layer);

        if(isColliding){
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            if(transform.position.y + 0.5f < collision.transform.position.y){
                collision.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                animGoomba.SetTrigger("Death");
                speed = 0;
                Destroy(gameObject, 0.3f);
                colliderGoomba.enabled = false;
                GameManager.Instance.AddScore(100);
            } else{
                if(PlayerMovement.isGrow){
                    PlayerMovement.isGrow = false;
                } else{
                    FindObjectOfType<PlayerMovement>().Death();

                    Goomba[] goomba = FindObjectsOfType<Goomba>();

                    for(int i = 0; i < goomba.Length; i++){
                        goomba[i].speed = 0;
                        goomba[i].animGoomba.speed = 0;
                    }
                }
            }
        }
    }



}
