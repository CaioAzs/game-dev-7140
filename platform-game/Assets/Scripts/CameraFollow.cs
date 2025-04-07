using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Transform player;
    [SerializeField] float minX,maxX;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if(player.position.x >= transform.position.x)
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), 0, transform.position.z);*/

        float targetX = Mathf.Clamp(player.position.x, minX, maxX);
        transform.position = new Vector3(targetX, 0, transform.position.z);
    }
}
