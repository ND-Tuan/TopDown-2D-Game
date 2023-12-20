using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AscendController : MonoBehaviour
{
    public Image Buff;
    public Text Exp;
    public Sprite[] BuffsList;
    public GameObject AnimationPanel;
    public GameObject Panel;
    private int BuffID;
    private int PreBuffID;
    private Player player;
    private WeaponHolder weaponHolder;
    public bool Open = true;

    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        if(Buff != null){
            BuffID = Random.Range(0,12);
            PreBuffID = BuffID;
            Buff.sprite = BuffsList[BuffID];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Exp != null) Exp.text = player.EXP.ToString();
    }

    public void Ascend(){
        if(player.EXP >= 1){
            ApplyBuff();
            player.EXP--;
            BuffID = RandomBuff();
            PreBuffID = BuffID;
            Buff.sprite = BuffsList[BuffID];
        }
    }

    public void RerollBuff(){
        if(player.CurCoin >=20){
            BuffID = RandomBuff();
            while(BuffID == PreBuffID){
                BuffID = RandomBuff();
            }
            PreBuffID = BuffID;
            Buff.sprite = BuffsList[BuffID];
            player.CurCoin -=20;
        }
    }

    void ApplyBuff(){
        if(BuffID ==0 || BuffID==4 || BuffID == 8){
            player.PlayerMaxHP = player.PlayerMaxHP + 1 + BuffID/4;
            player.AddHp(1 + BuffID/4);
        } else if(BuffID ==1 || BuffID==5 || BuffID == 9){
            weaponHolder.MaxMana += 5*(BuffID - 1)/4;
            weaponHolder.AddMana(5*(BuffID - 1)/4);
        } else if(BuffID ==2 || BuffID==6 || BuffID == 10){
            weaponHolder.CritRate = weaponHolder.CritRate + 2*(BuffID - 2)/4 + 1;
        } else if(BuffID ==3 || BuffID==7 || BuffID == 11){
            weaponHolder.CritDmg = weaponHolder.CritDmg + 2*(BuffID - 2)/4 + 1;
        }
    }

    public void Exit(){
        AnimationPanel.SetActive(true);
        AnimationPanel.GetComponent<AscendController>().Open = false;
        AnimationPanel.GetComponent<Animator>().SetBool("Close", true);
        Panel.SetActive(false);
    }

    public void Active(){
        if(Open){
            Panel.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }

    public void DeActive(){
        if(!Open){
            AnimationPanel.GetComponent<AscendController>().Open = true;
            AnimationPanel.SetActive(false);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>().BGPanel.SetActive(false);
            Time.timeScale =1;
        }
    }

    int RandomBuff(){
        int Rand = Random.Range(1,11);
        if(Rand>=1 && Rand<=6){
            return Random.Range(0,4);
        } else if(Rand>=7 && Rand<=9){
            return Random.Range(4,8);
        } else{
            return Random.Range(8,12);
        }
    }
}
