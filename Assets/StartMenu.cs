using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class StartMenu : MonoBehaviour
{
    public Text highScoreText;


    public void Start()
    {
        
        if (PlayerPrefs.GetString("HighScoreName") != ""){
        }
        highScoreText.text = "Maior pontuação feita por: " + PlayerPrefs.GetString("HighScoreName");
    }
//    + " " + PlayerPrefs.GetInt ("HighScore")
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("botao apertado para sair do jogo");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); //ira carregar para ir a tela principal do jogo

    }
}
