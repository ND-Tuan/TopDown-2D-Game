using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSword : MonoBehaviour
{
    public GameObject Slash;
    public  GameObject SlashPos;
    public GameObject WolfEffect;
    
    public float scale;
    public float duration;
    public float TimeBtwAttack;
    public float timeBtwAttack;
    public int SlashDmg;
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

        if(count==3){
            GameObject TmpWolfEffect =  Instantiate(WolfEffect, SlashPos.transform.position, WolfEffect.transform.rotation);

            TmpWolfEffect.transform.localScale = transform.localScale;
           
            Rigidbody2D rb = TmpWolfEffect.GetComponentInChildren<Rigidbody2D>();
            rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
            count = 0;
        }
    }

    void delay(){
        animator.SetBool("Attack", false);
    }
}
