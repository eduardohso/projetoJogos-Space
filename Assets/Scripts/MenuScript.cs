using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public TextMeshProUGUI pontuacaoText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pontuacaoText != null) pontuacaoText.text = "High Score:\n" + PlayerPrefs.GetInt("Score");
    }

    public void fechar(){
        Time.timeScale = 1;
        StartCoroutine(waitFechar());
    }
    IEnumerator waitFechar(){
        yield return new WaitForSeconds(.5f);
        Application.Quit();
    }

    public void Jogar(){
        Time.timeScale = 1;
        StartCoroutine(waitJogar());
    }
    IEnumerator waitJogar(){
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Level");
    }

    public void voltarMenu(){
        Time.timeScale = 1;
        StartCoroutine(waitVoltarMenu());
    }

    IEnumerator waitVoltarMenu(){
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Menu");
    }
}
