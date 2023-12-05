using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    public int Dmg;
    public bool DesByTrigger = true;
    private int DmgOutput;
    private bool IsCrit = false;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Wall") || other.CompareTag("Enemy")){
            if (DesByTrigger) Destroy(gameObject);
            if( other.CompareTag("Enemy")){
                WeaponHolder weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
                DmgOutput = weaponHolder.CritChange(Dmg);
                if(DmgOutput > Dmg){
                    IsCrit = true;
                }
                other.GetComponent<EnemyControll>().TakeDmg(DmgOutput, IsCrit);
                IsCrit = false;
            }
        }
        
        
    }
}
