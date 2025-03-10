using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int blocksLeft;

    public static GameManager Instance { get; private set; }

    private void Awake(){
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }

    void Start(){
        blocksLeft = GameObject.FindGameObjectsWithTag("Blocks").Length;

    }
    public void BlockDestroyed(){
        blocksLeft--;
        Debug.Log("Blocks left: " + blocksLeft);

        if (blocksLeft <= 1)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
{
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

    // Verifica se o próximo índice está dentro dos limites das cenas carregadas
    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
    else
    {
        Debug.Log("Último nível alcançado. Carregando o Menu Principal");
        SceneManager.LoadScene(0);
    }
}

    public void ReloadScene(){
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(4);
    }
}
