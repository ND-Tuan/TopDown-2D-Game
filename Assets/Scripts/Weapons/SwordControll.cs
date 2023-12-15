using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControll : MonoBehaviour
{
    public GameObject Slash;
    public Color SlashColor;
    public  GameObject SlashPos;
    public GameObject AirSlashEffect;
    public int CoutToAirSlash;
    public float scale;
    public float duration;
    public float TimeBtwAttack;
    public float timeBtwAttack;
    public int SlashDmg;
    public int AirSlasdDmg;
    public bool RotateSameToWeapon = false;
    public GameObject WeaponPos;
    public WeaponHolder weaponHolder;
    public int ManaCost;
    public Animator animator;
    private int count=0;

    public int BulletForce ;


    void Start(){
        WeaponPos =GameObject.FindGameObjectWithTag("WeaponPos");
        weaponHolder = WeaponPos.GetComponent<WeaponHolder>();
        weaponHolder.ShowManaCost(ManaCost);

        animator = GetComponentInChildren<Animator>();
        weaponHolder.Rotationable = true;
    }

    void Update()
    {
        timeBtwAttack -= Time.deltaTime;
       
        if(Input.GetMouseButton(0) && timeBtwAttack <=0 && weaponHolder.IsEnoughMana && Time.timeScale >0){
            Attack();
            weaponHolder.SubtractMana(ManaCost);

        }
    }

    void Attack(){
        timeBtwAttack = TimeBtwAttack;
        animator.SetBool("Attack", true);
        Invoke(nameof(InsSlash),0.2f);
        Invoke(nameof(delay), 0.43f);
        count++;
    }

    void InsSlash(){
        GameObject TmpSlash =  Instantiate(Slash, SlashPos.transform, worldPositionStays:false);
        TmpSlash.GetComponent<SlashControll>().Dmg = SlashDmg;
        TmpSlash.GetComponent<SpriteRenderer>().color = SlashColor;

        if(count==CoutToAirSlash){
            Quaternion rotation = AirSlashEffect.transform.rotation;
            if(RotateSameToWeapon) rotation = transform.rotation;
            GameObject Tmp =  Instantiate(AirSlashEffect, SlashPos.transform.position, rotation);

            Tmp.transform.localScale = transform.localScale;

            Tmp.GetComponent<BulletControll>().Dmg = AirSlasdDmg;
           
            Rigidbody2D rb = Tmp.GetComponentInChildren<Rigidbody2D>();
            rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
            count = 0;
        }
    }

    void delay(){
        animator.SetBool("Attack", false);
    }
}