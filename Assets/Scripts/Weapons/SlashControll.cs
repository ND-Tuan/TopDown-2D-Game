using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashControll : MonoBehaviour
{
   public int Dmg;
   private int DmgOutput;
   private bool IsCrit = false;

   void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("Enemy")){
            WeaponHolder weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
            DmgOutput = weaponHolder.CritChange(Dmg);
            if(DmgOutput > Dmg){
               IsCrit = true;
            }
            other.GetComponent<EnemyControll>().TakeDmg(DmgOutput, IsCrit);
            IsCrit = false;
        }
        if(other.CompareTag("EnemyBullet")) Destroy(other.gameObject);
        
   }
}
