using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public GameObject HpEffect;
    public GameObject MpEffect;
    public int PoisonType; //1-HP, 2-MP, 0-HP&MP
    public int HpAmount;
    public int MpAmount;
    private Player player;
    private WeaponHolder weaponHolder;
    private bool used= false;

    void Start(){
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        player= GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
    }
    
    void Update(){
        if(player!=null){
            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if(Input.GetKeyDown(KeyCode.R) && distanceToPlayer<5 && !used){
                GetComponent<AudioSource>().Play();
                if(PoisonType != 1){
                    weaponHolder.AddMana(MpAmount);
                    Instantiate(MpEffect, player.gameObject.transform, worldPositionStays:false);

                    GameObject instance = Instantiate(player.DmgPopup, gameObject.transform, worldPositionStays: false);
                    instance.GetComponent<TextMesh>().text ="+" +MpAmount;
                    instance.GetComponent<TextMesh>().color = new Color(0.2588235f, 0.5559593f, 1, 1);
                }
                if(PoisonType != 2){
                    player.AddHp(HpAmount);
                    Instantiate(HpEffect, player.gameObject.transform, worldPositionStays:false);
                }
                used = true;
                GetComponent<SpriteRenderer>().material.color = new Color(0,0,0,0);
                Destroy(gameObject,0.7f);
            }
        }
    }
}
