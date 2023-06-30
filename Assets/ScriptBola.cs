using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBola : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay; //true bola esta em jogo. False bola nao esta em jogo
    public Transform paddle; //paddle faz referencia com a PossicaoIncialBola (dentro do inspector)
    public float speed;
    public Transform explosao;
    public ScriptManager sm;
    public Transform powerup;
    AudioSource audio;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> (); //pega o componente Ridigbody
        audio = GetComponent<AudioSource> ();

    }

    // Update is called once per frame
    void Update()
    {
        if (sm.gameOver){
            return; //se der gameOver, ele retorna ele mesmo -> gameOver
        }
        if(!inPlay){
            transform.position = paddle.position;
        }

        if(Input.GetButtonDown ("Jump") && !inPlay) { //se estiver em jogo e pressionado o botao ele irá executar uma ves
            inPlay = true;
            rb.AddForce(Vector2.up * speed); //apos clicar espaço a jogada passa a ser True e aplicara uma força na bola para ser jogada para cima
        }
    }

    void OnTriggerEnter2D(Collider2D other){ // OnTrigger, quando um gameobject colide com outro gameobject, Unity chama OnTrigger
        if(other.CompareTag("bottom")){
            Debug.Log ("A bola saiu da tela!!");
            rb.velocity = Vector2.zero; //zera movimento da bola
            inPlay = false; //bola começa seguir a barra 
            sm.AtualizacaoVidas(-1); //perder uma vida caso encoste na bordaBaixo


        }

    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.transform.CompareTag("brick")) {
            BrickScript BrickScript = other.gameObject.GetComponent<BrickScript>(); //tijolo definido como gameobject
            if (BrickScript.toquesParaQuebrar > 1){
                BrickScript.quebrarTijolo();
            } else {
            int randChance = Random.Range(1, 101); //determinado para ver se um tijolo será um powerup ou não
            if (randChance < 50){ // se o randchance tiver valor gerado entre menos de 50% ele é instanciado na posição original do tijolo
                Instantiate(powerup, other.transform.position, other.transform.rotation);
            }
            Transform newExplosion = Instantiate (explosao, other.transform.position, other.transform.rotation); //instanciada uma explosão na rotação e posição do tijolo
            Destroy (newExplosion.gameObject, 2.5f); //destruir particulas geradas na hierarquia, 2.5f tempo para esperar 
            sm.AtualizacaoScore(BrickScript.points); //atualização dos pontos
            sm.AtualizacaoDosTijolos ();//atualizar numeros dos tijolos
            Destroy(other.gameObject); //tijolo destruido

        
            }
            audio.Play ();
            
        }
    }
}
