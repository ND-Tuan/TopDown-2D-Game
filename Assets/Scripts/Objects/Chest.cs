
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject ManaOrb;
    public GameObject Coin;
    public GameObject DropWeapon;
    public GameObject[] Poison;
    public Transform DropPos;
    public int ManaNum;
    public int CoinAmount;
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
        WeaponHolder  weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        for(int i=0; i<ManaNum; i++){
            GameObject Tmp = Instantiate(ManaOrb, DropPos.position, Quaternion.identity);
            Tmp.transform.position+= new Vector3(Random.Range(-2,3), Random.Range(-2,3), 0);
        }

        for(int i=0; i<CoinAmount; i++){
            GameObject Tmp = Instantiate(Coin, DropPos.position, Quaternion.identity);
            Tmp.transform.position+= new Vector3(Random.Range(-2,3), Random.Range(-2,3), 0);
        }

        if (Random.Range(0,10)<=4){
            int Rand = Random.Range(1,11);
            if(Rand>=1 && Rand<=6){
                WeaponID = Random.Range(0,5);
                
            } else if(Rand>=7 && Rand<=9){
                WeaponID = Random.Range(5,9);
                
            } else{
                WeaponID = Random.Range(9,11);
                
            }

            while(WeaponID ==  weaponHolder.Weapons[0] || WeaponID ==  weaponHolder.Weapons[1]){
                if(Rand>=1 && Rand<=6){
                    WeaponID = Random.Range(0,5);
                
                } else if(Rand>=7 && Rand<=9){
                    WeaponID = Random.Range(5,9);
                
                } else{
                    WeaponID = Random.Range(9,11);
                
                }
            }
            
            GameObject TmpW = Instantiate(DropWeapon, DropPos.position, Quaternion.identity);
            TmpW.GetComponent<WeaponDrop>().WeaponId = WeaponID;
        }

        if(Random.Range(0,10)<=2){
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
