using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControll : MonoBehaviour
{
    public bool IsEnemyBullet ;
    public bool IsGiveDmg = false;
    public float CastTime;
    public int Damage;
    private float DelayDmgTime = 0.4f;
    private float DelayDmgTimeCal=0;
    private bool DmgToPlayer = false ;
    private bool DmgToEnemy = false ;
    private Player player;
    public List<EnemyControll> enemyControll;

    void Update(){
        DelayDmgTimeCal -= Time.deltaTime;
        CastTime -= Time.deltaTime;

        if(CastTime<=0) IsGiveDmg = true;

        if(DelayDmgTimeCal <=0 && IsGiveDmg){
            if(DmgToPlayer){
                player.TakeDmg(1);           
            }
            if(DmgToEnemy){
                foreach(EnemyControll e in enemyControll){
                    if(e!=null){
                        e.TakeDmg(Damage, false, 0.15f);
                    } 
                }
            }
            DelayDmgTimeCal = DelayDmgTime;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other){
        
        
            if(IsEnemyBullet){

                if(other.CompareTag("Player")  ){
                    DmgToPlayer = true;
                    player = other.GetComponentInParent<Player>();
                }
            } else {
                if(other.CompareTag("Enemy")  ){
                    DmgToEnemy = true;
                    EnemyControll TmpEnemyControll = other.GetComponent<EnemyControll>();
                    if(TmpEnemyControll !=null){
                        enemyControll.Remove(TmpEnemyControll);
                        enemyControll.Add(TmpEnemyControll);   
                    } 
                }
            }
        
        
    }

    void OnTriggerExit2D(Collider2D other){

        if(IsEnemyBullet){
            if(other.CompareTag("Player" )  ){
                DmgToPlayer = false;
                
            }
        } else {
            if(other.CompareTag("Enemy")  ){
                DmgToEnemy = false;
                EnemyControll TmpEnemyControll = other.GetComponent<EnemyControll>();
                if(TmpEnemyControll !=null){
                    enemyControll.Remove(TmpEnemyControll);
                    DelayDmgTimeCal =0;
                }
            }
        }
        
    }

}
