using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControll : MonoBehaviour
{
    public GameObject Image;
    public GameObject Slash;
    public GameObject Stab;
    public Color SlashColor;
    public  GameObject SlashPos;
    public GameObject AirSlashEffect;
    public int CoutToAirSlash;
    public int CoutToAttack2;
    public float scale=1;
    public float duration;
    public float TimeBtwAttack;
    public float timeBtwAttack;
    public int SlashDmg;
    public int StabDmg;
    public int AirSlasdDmg;
    public bool RotateSameToWeapon = false;
    public GameObject WeaponPos;
    public WeaponHolder weaponHolder;
    public int ManaCost;
    public Animator animator;
    private int count=0;
    private int countAttack2 = 0;
    public int BulletForce ;
    private bool combo = false;


    void Start(){
        WeaponPos =GameObject.FindGameObjectWithTag("WeaponPos");
        weaponHolder = WeaponPos.GetComponent<WeaponHolder>();

        //hiển thị số mana tiêu tốn
        weaponHolder.ShowManaCost(ManaCost);

        animator = GetComponentInChildren<Animator>();
        weaponHolder.Rotationable = true;
    }

    void Update()
    {
        weaponHolder.Rotationable = true;
        timeBtwAttack -= Time.deltaTime;
        
       
        if(Input.GetMouseButton(0) && timeBtwAttack <=0 && weaponHolder.IsEnoughMana && Time.timeScale >0){

            countAttack2++; //đếm đến lượt thực hiện kiểu tấn công số 2

            if(countAttack2 == CoutToAttack2){
                Attack2();
            } else {
                Attack();
            }
            
            //trừ mana người chơi
            weaponHolder.SubtractMana(ManaCost);

        }
    }

    void Attack(){
        
        timeBtwAttack = TimeBtwAttack;
        animator.SetBool("Attack", true);

        //tạo nhát chém
        Invoke(nameof(InsSlash),0.2f);
        Invoke(nameof(delay), 0.43f);
        count++;
        
        GetComponent<Rotate_Weapon>().IsFlip = true;
        if(combo){
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3 (1,-1,0)); 
        } 
    }

    void Attack2(){
        timeBtwAttack = TimeBtwAttack;
        animator.SetBool("Attack2", true);
        Invoke(nameof(InsStab),0.05f);
        Invoke(nameof(delay), 0.25f);
        countAttack2 = 0;
    }

    void InsSlash(){
        
        //sinh ra nhát chém
        GameObject TmpSlash =  Instantiate(Slash, SlashPos.transform, worldPositionStays:false);

        //gắn chỉ số sát thương cho nhát chém
        TmpSlash.GetComponent<SlashControll>().Dmg = SlashDmg;

        //đổi màu nhát chém
        TmpSlash.GetComponent<SpriteRenderer>().color = SlashColor;
        TmpSlash.transform.localScale *= scale;

        //kiểm tra sinh bắn ra nhát chém không khí
        if(count==CoutToAirSlash){
            //quay nhát chém không khí theo hướng chém
            Quaternion rotation = AirSlashEffect.transform.rotation;
            if(RotateSameToWeapon) rotation = transform.rotation;
            GameObject Tmp =  Instantiate(AirSlashEffect, SlashPos.transform.position, rotation);

            //chỉnh kích cỡ nhát chém không khí
            Tmp.transform.localScale = transform.localScale ;

            //gắn sát thương
            Tmp.GetComponent<BulletControll>().Dmg = AirSlasdDmg;
           
            //gắn vector động lực
            Rigidbody2D rb = Tmp.GetComponentInChildren<Rigidbody2D>();
            rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
            count = 0;
        }

        combo = !combo;
    }

    //tạo chiêu đâm
    void InsStab(){
        GameObject TmpStab =  Instantiate(Stab, SlashPos.transform, worldPositionStays:false);

        //gắn sát thương
        TmpStab.GetComponentInChildren<SlashControll>().Dmg = StabDmg;

        //đổi màu nhát đâm
        TmpStab.GetComponentInChildren<SpriteRenderer>().color = SlashColor;

        //chỉnh kích cỡ
        TmpStab.transform.localScale *= scale;
    }

    void delay(){
        animator.SetBool("Attack", false);
        animator.SetBool("Attack2", false);
        GetComponent<Rotate_Weapon>().IsFlip = false;
    }
}
