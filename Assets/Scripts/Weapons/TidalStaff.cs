using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TidalStaff : MonoBehaviour
{
    public GameObject TidalDragon;
    public float CD;
    public int Dmg;
    public int ManaCost;
    private WeaponHolder weaponHolder;
    private float CDTmp;
    private GameObject Player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        weaponHolder.Rotationable = false;  
        weaponHolder.ShowManaCost(ManaCost);
        Player= GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CDTmp -= Time.deltaTime;
        if(Input.GetMouseButton(0) && CDTmp <0 && weaponHolder.IsEnoughMana && Time.timeScale >0){
            Invoke(nameof(Attack), 0.1f);
            animator.SetBool("Attack", true);
            Invoke(nameof(delay), 0.5f);
            CDTmp = CD;
            weaponHolder.SubtractMana(ManaCost);
        }
    }

    void delay(){
        animator.SetBool("Attack", false);
    }
    void Attack(){
        GameObject Tmp = Instantiate(TidalDragon, Player.transform, worldPositionStays:false);
        Tmp.GetComponent<BulletControll>().Dmg = Dmg;
    }
}
