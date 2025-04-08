using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BandeiraEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.Instance.score == 300 && GameManager.Instance.scoreCoin == 20 && GameManager.Instance.scoreMushroom == 1)
            {
                Invoke("NextLevel", 0);
            }
        }
    }

    void NextLevel(){
        Debug.Log("Carregando próxima fase...");
        SceneManager.LoadScene(3);
    }
}
