using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    private int index;

    public bool startDialog;
    public bool finishDialog;
    private bool isTyping = false;
    void Start()
    {
        textComponent.text = string.Empty;
        if(!startDialog){
            StartDialog();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            if(isTyping){
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            } else {
                 NextLine();
            } 
        }
        
    }

    void StartDialog(){
        startDialog = true;
        finishDialog = false;
        index = 0;
        Time.timeScale = 0f;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        isTyping = true;
        textComponent.text = string.Empty;

        foreach(char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }

        isTyping = false;
    }

    void NextLine(){
        if(index < lines.Length - 1){
            index++;
            StartCoroutine(TypeLine());
        } else {
            finishDialog = true;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }


}