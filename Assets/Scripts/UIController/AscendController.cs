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
            BuffID = Random.Range(0,12);
            PreBuffID = BuffID;
            Buff.sprite = BuffsList[BuffID];
        }
    }

    public void RerollBuff(){
        if(player.CurCoin >=20){
            BuffID = Random.Range(0,12);
            while(BuffID == PreBuffID){
                BuffID = Random.Range(0,12);
            }
            PreBuffID = BuffID;
            Buff.sprite = BuffsList[BuffID];
            player.CurCoin -=20;
        }
    }

    void ApplyBuff(){
        if(BuffID >=0 && BuffID<=2){
            player.PlayerMaxHP = player.PlayerMaxHP + 1 + BuffID;
            player.AddHp(1 + BuffID);
        } else if(BuffID >=3 && BuffID<=5){
            weaponHolder.MaxMana += 5*(BuffID - 2);
            weaponHolder.AddMana(5*(BuffID - 2));
        } else if(BuffID >=6 && BuffID<=8){
            weaponHolder.CritRate = weaponHolder.CritRate + 2*(BuffID-5) + 1;
        } else if(BuffID >=9 && BuffID<=11){
            weaponHolder.CritDmg = weaponHolder.CritDmg + 2*(BuffID-8) + 1;
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
}
