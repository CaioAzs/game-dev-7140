using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BandeiraEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Verifica os valores de score e scoreCoin no GameManager
            if (GameManager.Instance.score == 300 && GameManager.Instance.scoreCoin == 20 && GameManager.Instance.scoreMushroom == 1)
            {
                // Começa a Coroutine para mover e carregar nova cena
                Invoke("NextLevel", 0);
            }
        }
    }

    void NextLevel(){
        Debug.Log("Carregando próxima fase...");
        //SceneManager.LoadScene("Fase1");
        //Implementar a cena de Vitória!!!!!!!
    }
}
