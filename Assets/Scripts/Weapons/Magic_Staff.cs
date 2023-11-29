using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Staff : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject muzzle;
    public List<Transform> firePos;
    public float TimeBtwFire = 0.2f;
    public float BulletForce;
    private float timeBtwFire;
    public WeaponHolder weaponHolder;
    public int ManaCost;
    public int Damage ;

    void Start(){
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        weaponHolder.ShowManaCost(ManaCost);
    }

    void Update()
    {
        timeBtwFire -= Time.deltaTime;
        if(Input.GetMouseButton(0) && timeBtwFire <0 && weaponHolder.IsEnoughMana && Time.timeScale >0){

            Fire();
             weaponHolder.SubtractMana(ManaCost);
        }
    }

     void Fire(){
        timeBtwFire = TimeBtwFire;

        foreach(Transform p in firePos){
            GameObject BulletTmp = Instantiate(fireBall, p.transform.position, gameObject.transform.rotation);
            BulletTmp.GetComponent<BulletControll>().Dmg = Damage;
            Rigidbody2D rb = BulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
        }
    }
}
