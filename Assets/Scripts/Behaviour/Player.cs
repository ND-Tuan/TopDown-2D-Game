using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Vector3 moveInput;
    public  Animator animator;
    public float dashBoost = 2f;
    public static float dashTime;
    public float DashTime;
    private bool once;
    public float DashCD = 1f;
    public  float DashCDTmp;
    public SpriteRenderer  CharacterSR;
    public int PlayerMaxHP;
    private int PlayerCurHP;
    public Slider HpBar;
    public Image DashIcon;
    public Text text;
    public bool immune = false;
    public GameObject WeaponPos;
    public GameObject ArmPos;
    public GameObject KameHa;
    public float SkillDuration;
    public float SkillCD;
    public float ChargeTime;
    public float Energy=0;
    private GameObject pos2;
    public Image UltiIconCD;
    public Image UltiIconEnergy;
    private float SkillCDTmp =0;
    public bool InUltiTime = false;
    private GameObject TmpKameHa;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        WeaponPos =GameObject.FindGameObjectWithTag("WeaponPos");
        
        PlayerCurHP = PlayerMaxHP;
        
    }

    void Update(){

        //Hiển thị máu
        HpBar.value = PlayerCurHP;
        text.text = PlayerCurHP + "/" + PlayerMaxHP;

        Move();
        Dash();
        Skill();

    }

    void Move(){
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveSpeed * Time.deltaTime * moveInput; 

        animator.SetFloat("Speed", moveInput.sqrMagnitude);

         if (moveInput.x != 0)
            if (moveInput.x < 0){

                if(!InUltiTime){
                    CharacterSR.transform.localScale = new Vector3(-1, 1, 0); 
                } 
                WeaponPos.transform.localScale = new Vector3(-1, 1, 0);
                

                if(pos2 != null) pos2.transform.position = WeaponPos.transform.position + new Vector3((float)-4.5, 0, 0);
            }
            else{

                if(!InUltiTime) {
                    CharacterSR.transform.localScale = new Vector3(1, 1, 0);
                } 
                WeaponPos.transform.localScale = new Vector3(1, 1, 0);
                

                if(pos2 != null) pos2.transform.position = WeaponPos.transform.position + new Vector3((float)4.5, 0, 0);
            }
                
    }

    void Dash(){

        if(DashCDTmp >0){
            DashCDTmp -= Time.deltaTime;
        } else {
             DashCDTmp=0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0 && DashCDTmp==0)
        {
            WeaponPos.GetComponent<WeaponHolder>().RemoveWeapon();
            DashCDTmp = DashCD;
            animator.SetBool("Roll", true);
            
            immune = true;

            moveSpeed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once)
        {
            WeaponPos.GetComponent<WeaponHolder>().RestoreWeapon();
            animator.SetBool("Roll", false);
            
            immune = false;

            moveSpeed -= dashBoost;
            once = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }  

        //CD skill lướt
        DashIcon.fillAmount = 1-DashCDTmp/DashCD;
    }

    void Skill(){

        if(!InUltiTime) {
            

            if(SkillCDTmp >0){
                SkillCDTmp -= Time.deltaTime;
            } else {
                SkillCDTmp=0;
            }   
        }

        UltiIconCD.fillAmount = 1 - SkillCDTmp/SkillCD;
        UltiIconEnergy.fillAmount = Energy;

        if(Input.GetMouseButton(1) && SkillCDTmp==0){
            
            WeaponPos.GetComponent<WeaponHolder>().RemoveWeapon();
            animator.SetBool("Charge", true);

            if(Energy <1){
                if (ChargeTime<=0) ChargeTime=0.000001f;
                Energy += Time.deltaTime/ChargeTime;
            } else {
                Energy = 1;
            }

        } else{
            animator.SetBool("Charge", false);
            
            if(Energy==1){
                animator.SetBool("Blash", true);
                TmpKameHa = Instantiate(KameHa, ArmPos.gameObject.transform, worldPositionStays:false);
                SkillCDTmp = SkillCD;
                InUltiTime = true;
            } else if(!InUltiTime) {
                WeaponPos.GetComponent<WeaponHolder>().RestoreWeapon();
            }
            
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("EnemyBullet") && immune== false) {
            
            PlayerCurHP--;
            immune = true;

            CharacterSR.GetComponent<SpriteRenderer>().material.color = Color.red;
           
            Invoke(nameof(Nomal), 0.1f);
        }
        
    }

    void Nomal(){
       CharacterSR.GetComponent<SpriteRenderer>().material.color = Color.white;
       immune = false;
    }

    public void TakeDmg(){
        PlayerCurHP--;
    }
     

}
