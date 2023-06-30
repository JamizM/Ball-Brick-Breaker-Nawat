using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points; 
    public int toquesParaQuebrar;
    public Sprite hitSprite;


    public void quebrarTijolo(){        //deixar a função pública para ser chamada em outro script
        toquesParaQuebrar--;
        
    }

    

    // Start is called before the first frame update
    
}
