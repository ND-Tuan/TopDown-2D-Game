using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    public int Dmg;
    public bool DesByTrigger = true;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Wall") || other.CompareTag("Enemy")){
            if (DesByTrigger) Destroy(gameObject);
            if( other.CompareTag("Enemy")){
                other.GetComponent<EnemyControll>().TakeDmg(Dmg);
            }
        }
        
        
    }
}
