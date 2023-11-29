using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CallMenu : MonoBehaviour
{
    public GameObject BGPanel;
    public GameObject ShopMenu;

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
    
}
