using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgToPlayer : MonoBehaviour
{
    public int Dmg = 1;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.GetComponentInParent<Player>().TakeDmg(Dmg);
        }
    }

    void Destroy(){
        Destroy(gameObject);
    }
}
