using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashControll : MonoBehaviour
{
    public int Dmg;

   void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("Enemy")){
           other.GetComponent<EnemyControll>().TakeDmg(Dmg);
        }
        if(other.CompareTag("EnemyBullet")) Destroy(other.gameObject);
        
   }
}
