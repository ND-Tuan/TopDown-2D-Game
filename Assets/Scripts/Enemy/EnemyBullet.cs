using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public bool IsDestroy = true;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Player")){
            other.GetComponentInParent<Player>().TakeDmg(1);
        } 
        if((other.CompareTag("Player") || other.CompareTag("Wall")) && IsDestroy ) 
            Destroy(gameObject);
    }
}
