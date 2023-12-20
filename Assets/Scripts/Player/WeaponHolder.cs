using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public int[] Weapons;
    public GameObject defWe;
    public GameObject CurWeapon;
    public int CurSlot =0;
    public GameObject Drop;
    public UnityEngine.UI.Image WeaponIcon;
    public Slider ManaBar;
    public Text ManaValue;
    public int MaxMana;
    private int CurMana;
    private int Cost=0;
    public bool IsEnoughMana;
    public Text ManaCost;
    private bool NoWeaponMoment = false;
    private RoomTemplates templates;
    public bool Rotationable = true;
    public float CritRate = 25f;
    public float CritDmg = 30f;


    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke(nameof(SpawnWeapon), 0.5f);
        ManaBar.maxValue = MaxMana;
        CurMana = MaxMana;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !NoWeaponMoment && Time.timeScale >0){
            Destroy(CurWeapon);
            Rotationable = true;
            if(CurSlot==0){
                CurWeapon =  Instantiate(templates.WeaponsList[Weapons[1]], gameObject.transform, worldPositionStays:false);
                CurSlot =1;
            } else if (CurSlot==1){
                CurWeapon =  Instantiate(templates.WeaponsList[Weapons[0]], gameObject.transform, worldPositionStays:false);
                CurSlot=0;
            }
            WeaponIcon.sprite =CurWeapon.GetComponent<SpriteRenderer>().sprite;
            WeaponIcon.SetNativeSize();
            
        }

        ManaBar.value = CurMana;
        ManaBar.maxValue = MaxMana;

        ManaValue.text = CurMana + "/" + MaxMana;

        if(Cost > CurMana){
            ManaCost.color= Color.red;
            IsEnoughMana = false;
        } else{
            ManaCost.color= Color.white;
            IsEnoughMana = true;
        }
        if(Rotationable == false){
            gameObject.transform.localScale = new Vector3(1, 1, 0);  
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

    public async void AddMana(int Amount){
       
        
        if(CurMana<MaxMana) {
            while(Amount >0){
                CurMana++;
                Amount--;
                await Task.Delay(20);
            }
        }
        if(CurMana>MaxMana) CurMana = MaxMana;
        
    }

    public int CritChange (int Dmg){
        int Rand = Random.Range(1,101);

        if(Rand <= CritRate){
            Dmg = Mathf.RoundToInt(Dmg + Dmg*(CritDmg/100));
        }
        
        return Dmg;
    }

    public void RemoveWeapon(){
        CurWeapon.SetActive(false);
        NoWeaponMoment  = true;
    }

    public void RestoreWeapon(){
        if(NoWeaponMoment){
            CurWeapon.SetActive(true);
            NoWeaponMoment = false;
        }
        
    }

    void SpawnWeapon(){
        CurWeapon =  Instantiate(templates.WeaponsList[Weapons[CurSlot]], gameObject.transform, worldPositionStays:false);
        WeaponIcon.sprite =CurWeapon.GetComponent<SpriteRenderer>().sprite;
        WeaponIcon.SetNativeSize();
        if(GetComponentInChildren<Rotate_Weapon>() == null) Rotationable = false;
    }

    public void DropWeapon(int WeaponID){
        Destroy(CurWeapon);
        GameObject DropTmp = Instantiate(Drop, transform.position, Drop.transform.rotation);
        DropTmp.GetComponent<WeaponDrop>().WeaponId = Weapons[CurSlot];
        Weapons[CurSlot] = WeaponID;
        SpawnWeapon();
    }
}
