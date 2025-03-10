using UnityEngine;

public class LooseWall : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collision){
        GameManager.Instance.ReloadScene();
    }
}