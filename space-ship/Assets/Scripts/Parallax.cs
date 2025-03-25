using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float height; 
    public float parallaxEffect;

    void Start()
    {
        height = GetComponent<SpriteRenderer>().bounds.size.y; 
    }

    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * parallaxEffect; 

        if (transform.position.y < -height)
        {
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }
}