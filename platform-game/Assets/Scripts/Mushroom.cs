using UnityEngine;

public class Mushroom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] float speed;

    [SerializeField] bool moveLeft;

    void Start()
    {
        moveLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveLeft){
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        } else{
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == 3){
            if(!moveLeft){
                moveLeft = true;
            } else{
                moveLeft = false;
            }
        }

        if(collision.CompareTag("Player")){
            PlayerMovement.isGrow = true;
            Destroy(gameObject);
        }
    }

}
