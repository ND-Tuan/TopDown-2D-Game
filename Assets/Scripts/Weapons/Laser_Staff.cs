using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Staff : MonoBehaviour
{
    
    public GameObject laser;
    public Transform firePos;
    public float TimeBtwFire = 0.2f;
    private float timeBtwFire;
    public int ManaCost;
    public WeaponHolder weaponHolder;


    void Start(){
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        weaponHolder.ShowManaCost(ManaCost);
    }

    // Update is called once per frame
    void Update()
    {
        timeBtwFire -= Time.deltaTime;
        if(Input.GetMouseButton(0) && timeBtwFire <0){

            Fire();
            weaponHolder.SubtractMana(ManaCost);
        }
    }

    void Fire(){
        timeBtwFire = TimeBtwFire;

        GameObject BulletTmp = Instantiate(laser, firePos.transform, worldPositionStays:false);

        BulletTmp.GetComponent<LaserControll>().IsEnemyBullet = false;
        BulletTmp.GetComponent<LaserControll>().ManaCost = ManaCost;
        BulletTmp.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        
    }
}
