using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Staff : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject muzzle;
    public Transform firePos;
    public float TimeBtwFire = 0.2f;
    public float BulletForce;
    private float timeBtwFire;
    public WeaponHolder weaponHolder;
    public int ManaCost;


    void Start(){
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        weaponHolder.ShowManaCost(ManaCost);
    }

    void Update()
    {
        timeBtwFire -= Time.deltaTime;
        if(Input.GetMouseButton(0) && timeBtwFire <0 && weaponHolder.IsEnoughMana){

            Fire();
             weaponHolder.SubtractMana(ManaCost);
        }
    }

     void Fire(){
        timeBtwFire = TimeBtwFire;

        GameObject BulletTmp = Instantiate(fireBall, firePos.transform.position, gameObject.transform.rotation);
        Rigidbody2D rb = BulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
        

    }
}
