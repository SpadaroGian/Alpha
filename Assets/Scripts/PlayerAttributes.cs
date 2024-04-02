using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public int playerHp;
    // Start is called before the first frame update
   
    private void OnTriggerEnter(Collider collision){
        if( collision.gameObject.name == "Ghost")
        {
            playerHp -= 1;
            if ( playerHp <= 0)
            {
                Object.Destroy(this.gameObject);
            }
        }
    }
}
