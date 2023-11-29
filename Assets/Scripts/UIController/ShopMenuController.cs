using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    public Image[] ItemIcons;
    public Text[] Price;
    public int[] ItemsID ;
    public GameObject WeaponDrop;
    public bool IsPoison;
    public Text CurCoin;
    private int CurCoinInt;
    private int CurChoose = -1;
    public GameObject WeaponShop;
    public GameObject PoisonShop;


    // Update is called once per frame
    void Update()
    {
        CurCoinInt = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>().CurCoin;
        CurCoin.text = CurCoinInt.ToString();
    }

    public void Choose0(int i){
        CurChoose =i;
        if(CurCoinInt < int.Parse(Price[CurChoose].text)){
            CurCoin.color = Color.red;
        } else {
            CurCoin.color = new Color(0.03137255f, 0.572549f, 0.4196079f);
        }
    }
    public void Buy(){
        
        if(CurChoose>=0 ){
            if(CurCoinInt >= int.Parse(Price[CurChoose].text)){
                RoomTemplates roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
                Player player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();

                if(IsPoison){
                    GameObject tmp = Instantiate(roomTemplates.Poison[ItemsID[CurChoose]], transform.position, Quaternion.identity);
                    tmp.transform.position+= new Vector3(Random.Range(-5,6), Random.Range(-2,3)-10, 0);
    
                } else {
                    GameObject TmpW = Instantiate(WeaponDrop,transform.position, Quaternion.identity);
                    TmpW.GetComponent<WeaponDrop>().WeaponId = ItemsID[CurChoose];
                    TmpW.transform.position+= new Vector3(Random.Range(-5,6), Random.Range(-2,3)-15, 0);
                }

                player.CurCoin -= int.Parse(Price[CurChoose].text);
            }
        }
    }

}
