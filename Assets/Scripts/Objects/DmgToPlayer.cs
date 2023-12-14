using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgToPlayer : MonoBehaviour
{
    public int Dmg = 1;
    public bool IsDestroyPlayerBullet = false;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.GetComponentInParent<Player>().TakeDmg(Dmg);
        }

        if(other.CompareTag("bullet") && IsDestroyPlayerBullet) Destroy(other.gameObject);
    }

    void Destroy(){
        Destroy(gameObject);
    }
}
