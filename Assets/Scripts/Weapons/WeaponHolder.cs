using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public GameObject[] Weapons;
    public GameObject defWe;
    public GameObject CurWeapon;
    private int CurSlot =0;
    public UnityEngine.UI.Image WeaponIcon;
    public Slider ManaBar;
    public Text ManaValue;
    private int CurMana;
    private int Cost=0;
    public bool IsEnoughMana;
    public Text ManaCost;
    private bool NoWeaponMoment = false;


    // Start is called before the first frame update
    void Start()
    {
       Invoke(nameof(SpawnWeapon), 0.5f);
       CurMana = 150;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !NoWeaponMoment){
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

        if(Cost > CurMana){
            ManaCost.color= Color.red;
            IsEnoughMana = false;
        } else{
            ManaCost.color= Color.white;
            IsEnoughMana = true;
        }
    }

    public void ShowManaCost(int cost){
        ManaCost.text = cost.ToString();
        Cost = cost;
    }

    public async void SubtractMana(int cost){
        
        while(cost >0){
            CurMana--;
            cost--;
            await Task.Delay(20);
        }
    }

    public void AddMana(){
        if(CurMana<150) CurMana++;
        
    }

    public void RemoveWeapon(){
        Destroy(CurWeapon);
        NoWeaponMoment  = true;
    }

    public void RestoreWeapon(){
        if(NoWeaponMoment){
            CurWeapon =  Instantiate(Weapons[CurSlot], gameObject.transform, worldPositionStays:false);
            NoWeaponMoment = false;
        }
        
    }

    void SpawnWeapon(){
        CurWeapon =  Instantiate(Weapons[0], gameObject.transform, worldPositionStays:false);
       WeaponIcon.sprite =CurWeapon.GetComponent<SpriteRenderer>().sprite;
       WeaponIcon.SetNativeSize();
    }
}
