using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other){
        bool immune;
        if(other.GetComponent<Player>() != null){
            immune = other.GetComponent<Player>().immune;
        } else{
            immune = false;
        }

        if((other.CompareTag("Player") || other.CompareTag("Wall")) && immune==false ) 
            Destroy(gameObject);
    }
}
