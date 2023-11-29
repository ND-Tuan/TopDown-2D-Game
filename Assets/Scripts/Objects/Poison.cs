using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public int PoisonType; //1-HP, 2-MP, 0-HP&MP
    public int HpAmount;
    public int MnAmount;
    private Player player;
    private WeaponHolder weaponHolder;

    void Start(){
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        player= GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
    }
    
    void Update(){
        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if(Input.GetKeyDown(KeyCode.R) && distanceToPlayer<5){
            if(PoisonType != 1){
                weaponHolder.AddMana(MnAmount);
            }
            if(PoisonType != 2){
                player.AddHp(HpAmount);
            }
            Destroy(gameObject);
        }
    }
}
