using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBloom : MonoBehaviour
{
    public GameObject Attack;
    public GameObject firePos;
    private RoomTemplates roomTemplates;
    private int EnemyCurHp ;
    public int EnemyMaxHp;
    public GameObject HealthBar;
    public GameObject Health;
    public GameObject Main;


    void Start()
    {
        InvokeRepeating(nameof(ActiveAttack), 1f, 4f);
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        EnemyCurHp = EnemyMaxHp;
    }

    void Update(){
        if(roomTemplates.countEnemy == 0){
            Destroy(Main);
        }
    }

    void ActiveAttack(){
        Instantiate(Attack, firePos.transform.position, firePos.transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("bullet")) {
            EnemyCurHp --;     

            double HealAmount = EnemyCurHp *(1.2/EnemyMaxHp);    
            Health.transform.localScale = new Vector3((float)HealAmount, (float)1.6, 0);
            
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
            Invoke(nameof(Nomal), 0.1f);
        }
        if( EnemyCurHp ==0) {
         
            Destroy(Main);
 
        }
    }

    void Nomal(){
       gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
    }
}
