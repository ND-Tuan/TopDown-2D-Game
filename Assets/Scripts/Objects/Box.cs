using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject Break;
    public GameObject[] poison;
    private int durable=3;
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("EnemyBullet") || other.CompareTag("bullet") || other.CompareTag("DmgGiver")){
            durable--;
            if(durable<=0){
                Instantiate(Break, gameObject.transform.position, Quaternion.identity);
                if(Random.Range(1,11)==1) Instantiate(poison[Random.Range(0,3)], gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
