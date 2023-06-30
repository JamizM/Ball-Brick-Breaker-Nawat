using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraBaixoScript : MonoBehaviour

{
    public float speed;
    public float leftScreenEdge;
    public float rightScreenEdge;
    public ScriptManager sm;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sm.gameOver){ 
            return; //interrompe qualquer processamento adicional neste quadro, ou seja, paddle do jogo não se move ao terminar o jogo
        }
        float horizontal = Input.GetAxis("Horizontal"); //retorna valor negativo ao boneco andar para esquerda, e valor positivo ao andar para direita

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed); //usa o horizontal para mover-se para esquerda, mesmo com o Vector2.right
        if(transform.position.x < leftScreenEdge) {
            transform.position = new Vector2(leftScreenEdge, transform.position.y); //necessário definir os pontos da tela dentro da unity 
        }

        if(transform.position.x > rightScreenEdge) {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }    

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExtraLife")){
            sm.AtualizacaoVidas (1);
            Destroy(other.gameObject);
        }
    }
    
}
