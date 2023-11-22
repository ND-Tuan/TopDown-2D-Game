
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject ManaOrb;
    public GameObject DropWeapon;
    public GameObject[] Poison;
    public Transform DropPos;
    public int ManaNum;
    public int WeaponID;
    private Animator animator;
    private bool isDrop= false;

    void Start(){
        animator = GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !isDrop){
            animator.SetBool("Open", true);
            Invoke(nameof(Drop), 0.75f);
            Invoke(nameof(SelfDes), 2.5f);
            isDrop = true;
        }
    }

    void Drop(){
        for(int i=0; i<ManaNum; i++){
            GameObject Tmp = Instantiate(ManaOrb, DropPos.position, Quaternion.identity);
            Tmp.transform.position+= new Vector3(Random.Range(-2,3), Random.Range(-2,3), 0);
        }

        if (Random.Range(0,10)==1){
            WeaponID = Random.Range(0,5);
            GameObject TmpW = Instantiate(DropWeapon, DropPos.position, Quaternion.identity);
            TmpW.GetComponent<WeaponDrop>().WeaponId = WeaponID;
        }

        if(Random.Range(0,10)==1){
            int Rand = Random.Range(1,11);
            if(Rand>=1 && Rand<=6){
                Instantiate(Poison[Random.Range(0,3)], DropPos.position, Quaternion.identity);
            } else if(Rand>=7 && Rand<=9){
                Instantiate(Poison[Random.Range(3,6)], DropPos.position, Quaternion.identity);
            } else{
                Instantiate(Poison[Random.Range(6,9)], DropPos.position, Quaternion.identity);
            }
        }
        
    }

    void SelfDes(){
        Destroy(gameObject);
    }
   
}
