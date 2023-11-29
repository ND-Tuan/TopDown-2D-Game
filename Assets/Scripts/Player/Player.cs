using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
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
    public int PlayerCurHP;
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
    private bool appear = false;
    public int CurCoin =0;
    public Text Coin;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        WeaponPos =GameObject.FindGameObjectWithTag("WeaponPos");
        
        PlayerCurHP = PlayerMaxHP;
        Invoke(nameof(Appear), 0.5f); 
    }

    void Update(){

        //Hiển thị máu
        HpBar.maxValue = PlayerMaxHP;
        HpBar.value = PlayerCurHP;
        text.text = PlayerCurHP + "/" + PlayerMaxHP;
        Coin.text = CurCoin.ToString();

        if(PlayerCurHP <=0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if(appear && Time.timeScale >0){
            Move();
            Dash();
            Skill();
        }
    }

    void Move(){
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        if(moveInput.x!=0 && moveInput.y!=0) moveInput.Normalize(); //Chuẩn hóa vector khi nv di chuyển trên đường chéo

        transform.position += moveSpeed * Time.deltaTime * moveInput;

        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        //xác định qóc quay của chuột đối vs người chơi
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        //Đổi hướng nhân vật
        if ( !InUltiTime)
            if ((rotation.eulerAngles.z > 90 && rotation.eulerAngles.z < 270 && WeaponPos.GetComponent<WeaponHolder>().Rotationable)
                    || (moveInput.x <0 && WeaponPos.GetComponent<WeaponHolder>().Rotationable ==false)){  
                CharacterSR.transform.localScale = new Vector3(-1, 1, 0);
                if(WeaponPos.GetComponent<WeaponHolder>().Rotationable) 
                    WeaponPos.transform.localScale = new Vector3(-1, 1, 0);
            }
            else if(moveInput.x != 0 || WeaponPos.GetComponent<WeaponHolder>().Rotationable){             
                CharacterSR.transform.localScale = new Vector3(1, 1, 0);
                if(WeaponPos.GetComponent<WeaponHolder>().Rotationable)
                    WeaponPos.transform.localScale = new Vector3(1, 1, 0);  
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
           
            DashCDTmp = DashCD;
            animator.SetBool("Roll", true);
            
            immune = true;

            moveSpeed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once)
        {
            //WeaponPos.GetComponent<WeaponHolder>().RestoreWeapon();
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
            
            if(Energy==1 && !InUltiTime){
                animator.SetBool("Blash", true);
                TmpKameHa = Instantiate(KameHa, ArmPos.gameObject.transform, worldPositionStays:false);
                SkillCDTmp = SkillCD;
                InUltiTime = true;
            } else if(!InUltiTime) {
                WeaponPos.GetComponent<WeaponHolder>().RestoreWeapon();
            }
        }
    }

    void Nomal(){
       CharacterSR.GetComponent<SpriteRenderer>().material.color = Color.white;
       immune = false;
    }

    public void TakeDmg(){
        if(immune ==  false){
            PlayerCurHP--;
            immune = true;
            CharacterSR.GetComponent<SpriteRenderer>().material.color = Color.red;
            Invoke(nameof(Nomal), 0.1f);
        } 
    }

    void Appear(){
        animator.SetBool("Appear", true);
        appear = true;
    }

    public async void AddHp(int Amount){
        if(PlayerCurHP<PlayerMaxHP) {
            while(Amount >0){
                PlayerCurHP++;
                Amount--;
                await Task.Delay(20);
            }
        }
        if(PlayerCurHP>PlayerMaxHP) PlayerCurHP=PlayerMaxHP;
    }

    public void ChangeCoinAmount(int Amount){
        CurCoin += Amount;
    }

    
     

}
