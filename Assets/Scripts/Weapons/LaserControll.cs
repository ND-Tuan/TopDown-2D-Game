using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControll : MonoBehaviour
{
    public bool IsEnemyBullet ;
    public bool IsGiveDmg = false;
    public float CastTime;
    public int Damage;
    public float DelayDmgTime;
    private float DelayDmgTimeCal;
    public int ManaCost;
    public WeaponHolder weaponHolder;



    // Start is called before the first frame update

    void Start()
    {
        Invoke(nameof(GiveDmg), 0.75f);
    }
    
    void OnTriggerEnter2D(Collider2D other){
        
        if(IsEnemyBullet ==true){
            if(other.CompareTag("Player") && IsGiveDmg){
                other.GetComponent<Player>().TakeDmg();
            }
        } else {
            if(other.CompareTag("Enemy") && IsGiveDmg==true){
                other.GetComponent<EnemyControll>().TakeDmg(Damage);
                DelayDmgTimeCal = DelayDmgTime;
                
            }
        }
    }

    void OnTriggerStay2D(Collider2D other){

        DelayDmgTimeCal -= Time.deltaTime;

        if(IsEnemyBullet){

            if(other.CompareTag("Player" ) && IsGiveDmg && DelayDmgTimeCal <=0){
                other.GetComponent<Player>().TakeDmg();
                DelayDmgTimeCal = DelayDmgTime;
            }

        } else {

            if(other.CompareTag("Enemy") && IsGiveDmg && DelayDmgTimeCal <=0){
                other.GetComponent<EnemyControll>().TakeDmg(Damage);
                DelayDmgTimeCal = DelayDmgTime;
            }
        }
    }

    void GiveDmg(){
        IsGiveDmg = true;
    }
}
