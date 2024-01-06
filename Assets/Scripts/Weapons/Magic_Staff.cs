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
    public Animator animator;
    public bool Rotationable = true;

    void Start(){
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        weaponHolder.ShowManaCost(ManaCost);
        
        weaponHolder.Rotationable = Rotationable;
    
    }

    void Update()
    {
        //Đếm ngược
        timeBtwFire -= Time.deltaTime;

        weaponHolder.Rotationable = Rotationable;
        if(Input.GetMouseButton(0) && timeBtwFire <0 && weaponHolder.IsEnoughMana && Time.timeScale >0){

            if(animator != null){
                animator.SetBool("Attack", true);
            }
            Fire();
            weaponHolder.SubtractMana(ManaCost); //trừ mana người chơi
        }
    }

     void Fire(){
        timeBtwFire = TimeBtwFire;
        GetComponent<AudioSource>().Play(); //chạy âm thanh vũ khí

        foreach(Transform p in firePos){
            //sinh ra viên đạn tại vị trí bắn
            GameObject BulletTmp = Instantiate(fireBall, p.transform.position, p.transform.rotation);

            //gắn giá trị sát thương cho đạn
            BulletTmp.GetComponent<BulletControll>().Dmg = Damage;

            //gắn vector cho đạn
            Rigidbody2D rb = BulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(p.transform.right * BulletForce, ForceMode2D.Impulse);
            
            if(animator != null){
                Invoke(nameof(EndAttack), 0.5f);
            }
        }
    }

    void EndAttack(){
        animator.SetBool("Attack", false);
    }
}
