using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultMenuController : MonoBehaviour
{
    private CallMenu callMenu;
    public Image[] WeaponIcon;
    public Text time;
    public Text date;
    void Start()
    {
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
        callMenu.UnDisplayEnding();
        date.text = DateTime.Now.ToShortDateString();
        WeaponHolder weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        RoomTemplates roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        for(int i = 0; i<2; i++){
            WeaponIcon[i].sprite = roomTemplates.WeaponsList[weaponHolder.Weapons[i]].GetComponent<SpriteRenderer>().sprite;
            WeaponIcon[i].SetNativeSize();
        }
    }

    public void BackHome(){
        Time.timeScale = 1;
        callMenu.DestroyToReset();
        SceneManager.LoadSceneAsync(0);
    }

    
}
