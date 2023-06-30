using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //usado para mexer com os textos
using UnityEngine.SceneManagement; //usado para o controle de gerenciamento de cena

public class ScriptManager : MonoBehaviour
{
    public int lives;
    public int scores;
    public Text LivesText;
    public Text ScoreText;
    public bool gameOver;
    public GameObject gameOverPainel;
    public int numeroDeTijolos;
    public Transform [] levels;  //[] -> array de transforms, aonde os levels serão alocados
    public int currentLevelIndex =0;
    public GameObject PainelDeTelaDeCarregamento;
    public Text highScoreText;
    public InputField highScoreInput;


    // Start is called before the first frame update
    void Start()
    {
        LivesText.text = "Lives: " + lives;
        ScoreText.text = "Score: " + scores;
        numeroDeTijolos = GameObject.FindGameObjectsWithTag("brick").Length; //comando faz com que ache todos os objetos que possua a tag "brick"
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizacaoDosTijolos()
    {
        numeroDeTijolos--;
        if (numeroDeTijolos <= 0){   //ao ficar sem tijolos iremos verificar se há algum tijolo ainda na tela
            if (currentLevelIndex >= levels.Length - 1){ //para não ir ao level 3 após terminar a 2 fase, ou seja, além do último disponivel
                GameOver();
        } else {
            PainelDeTelaDeCarregamento.SetActive (true); //ativa e mostra painel de tela de carregamento para próximo fase
            PainelDeTelaDeCarregamento.GetComponentInChildren<Text>().text = "Level: " + (currentLevelIndex + 2); //texto exibido será "Level: " seguido pelo número do próximo nível.
            gameOver = true;
            Invoke("CarregarNivel", 3f); //demora 3 segundos para aparecer o próximo jogo
        }
        
        }
    }
    public void CarregarNivel()
    {
        currentLevelIndex++; //avança nivel
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity); //se caso tenha tijolos, iremos instanciar o proximo nivel  
        numeroDeTijolos = GameObject.FindGameObjectsWithTag("brick").Length; //mostra quantos objetos tem dentro de uma cena 
        gameOver = false;
        PainelDeTelaDeCarregamento.SetActive (false);
    }
    public void AtualizacaoVidas(int mudarVidas)
    {
        lives += mudarVidas;
        if (lives <= 0){
            lives = 0;
            GameOver ();
        }
        LivesText.text = "Lives: " + lives;
    }
 
    public void AtualizacaoScore(int mudarScore)
    {
        scores += mudarScore;
        ScoreText.text = "Score: " + scores;
    }
    void GameOver()
    {
        gameOver = true;
        gameOverPainel.SetActive (true); //ativar painel de gameover 
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (scores > highScore){
            PlayerPrefs.SetInt("HighScore: ", scores);
            highScoreText.text = "HighScore!" + "\n" + "Digite seu nome Abaixo";
            highScoreInput.gameObject.SetActive (true); //onde jogador ira colocar o nome
        }else{
            highScoreText.text = PlayerPrefs.GetString("HighScoreName") + "Seu score foi: " + highScore + "\n" + "Voce pode vence-lo?"; 
        }
    }
    public void NewHighScore() //função que é chamado quando o jogador digita seu nome no campo highScoreInput e pressiona um botão para confirmar.
    {
        string highScoreName = highScoreInput.text; 
        PlayerPrefs.SetString("HighScoreName", highScoreName); //PlayerPrefs -> função de armazenar e acessar dados
        highScoreInput.gameObject.SetActive (false);
        highScoreText.text = "Parabéns " + highScoreName + "\n" + "Seu novo score é: " + scores;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene"); //SampleScene principal janela de unidade para carregar a tela. E tudo que estiver naquela cena vai se repetir
    }
    public void Sair()
    {
        SceneManager.LoadScene("StartMenu"); //função de sair do jogo
    }
}
