using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public GameObject[] Weapons;
    public GameObject defWe;
    private GameObject CurWeapon;
    private int CurSlot =0;
    public UnityEngine.UI.Image WeaponIcon;
    public Slider ManaBar;
    public Text ManaValue;
    private int CurMana;
    public Text ManaCost;


    // Start is called before the first frame update
    void Start()
    {
       CurWeapon =  Instantiate(Weapons[0], gameObject.transform, worldPositionStays:false);
       WeaponIcon.sprite =CurWeapon.GetComponent<SpriteRenderer>().sprite;
       WeaponIcon.SetNativeSize();
       CurMana = 150;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            Destroy(CurWeapon);
            if(CurSlot==0){
                CurWeapon =  Instantiate(Weapons[1], gameObject.transform, worldPositionStays:false);
                CurSlot =1;
            } else if (CurSlot==1){
                CurWeapon =  Instantiate(Weapons[0], gameObject.transform, worldPositionStays:false);
                CurSlot=0;
            }
            WeaponIcon.sprite =CurWeapon.GetComponent<SpriteRenderer>().sprite;
            WeaponIcon.SetNativeSize();
        }

        ManaBar.value = CurMana;

        ManaValue.text = CurMana + "/" + 150;
    }

    public void ShowManaCost(int cost){
        ManaCost.text = cost.ToString();
    }

    public void SubtractMana(int cost){
        CurMana -= cost;
    }
}
