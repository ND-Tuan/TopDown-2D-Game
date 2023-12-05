using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CallMenu : MonoBehaviour
{
    public float TotalTime;
    public GameObject BGPanel;
    public GameObject ShopMenu;
    public GameObject ChangeScene;
    public GameObject Ending;
    public GameObject EndingPanel;
    public GameObject ResultMenu;
    public GameObject PauseMenu;
    public GameObject[] ObjectsDestroyToReset;

    void Update(){
        TotalTime += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.P) && PauseMenu.activeSelf ==  false){
            PauseGame();
        }
    }

    public void DestroyToReset(){
        foreach(GameObject Object in ObjectsDestroyToReset){
            Destroy(Object);
        }
    }
    
    public void DisplayShopMenu(bool display){
        BGPanel.SetActive(display);
        ShopMenu.SetActive(display);
        if(display){
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1;
        }
    }

    public void SetItemForShop(int ID,int i, int price, GameObject SR, bool IsPoison){
        ShopMenuController shop = ShopMenu.GetComponent<ShopMenuController>();

        shop.ItemsID[i] = ID;
        shop.ItemIcons[i].sprite =SR.GetComponent<SpriteRenderer>().sprite;
        shop.ItemIcons[i].SetNativeSize();
        shop.Price[i].text = price.ToString();
        shop.IsPoison =IsPoison;

        if(IsPoison){
            shop.WeaponShop.SetActive(false);
            shop.PoisonShop.SetActive(true);
        } else{
            shop.WeaponShop.SetActive(true);
            shop.PoisonShop.SetActive(false);
        }
    }

    public void DisplayChange(){
        ChangeScene.SetActive(true);
        Time.timeScale= 0;
    }

    public void DisplayEnding(){
        Ending.SetActive(true);
        EndingPanel.SetActive(true);
    }

    public void UnDisplayEnding(){
        Ending.SetActive(false);
    }

    public void DisplayResult(){
        ResultMenu.SetActive(true);
        UpdateLevelTimer(TotalTime);
    }

    void UpdateLevelTimer(float totalSeconds){
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);
    
        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }
        ResultMenuController resultMenuController = ResultMenu.GetComponent<ResultMenuController>();
        resultMenuController.time.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void PauseGame(){
        Time.timeScale = 0;
        BGPanel.SetActive(true);
        PauseMenu.SetActive(true);
        PausedMenuControll pausedMenuControll = PauseMenu.GetComponent<PausedMenuControll>();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
        WeaponHolder  weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>(); 

        pausedMenuControll.MaxHp.text = player.PlayerMaxHP.ToString();
        pausedMenuControll.MaxMp.text = weaponHolder.MaxMana.ToString();
        pausedMenuControll.CritRate.text = weaponHolder.CritRate+ "%";
        pausedMenuControll.CritDmg.text = weaponHolder.CritDmg+ "%";
    }
    
}
