using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
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
    public float SkillDuration;
    public float SkillCD;
    private GameObject pos2;
    


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
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
                CharacterSR.transform.localScale = new Vector3(-1, 1, 0);
                WeaponPos.transform.localScale = new Vector3(-1, 1, 0);

                if(pos2 != null) pos2.transform.position = WeaponPos.transform.position + new Vector3((float)-4.5, 0, 0);
            }
            else{
                CharacterSR.transform.localScale = new Vector3(1, 1, 0); 
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
            DashCDTmp = DashCD;
            animator.SetBool("Roll", true);
            immune = true;

            moveSpeed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once)
        {
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

    async void Skill(){
        if(Input.GetKeyDown(KeyCode.E)){
            WeaponPos = GameObject.FindGameObjectWithTag("WeaponPos");
            pos2 = Instantiate(WeaponPos.GetComponent<WeaponHolder>().CurWeapon, WeaponPos.gameObject.transform, worldPositionStays:false);
            pos2.transform.position = WeaponPos.transform.position + new Vector3((float)4.5, 0, 0);

            float duration = SkillDuration;
            while(duration>0) {
                duration -= Time.deltaTime;
                if(duration<=0) Destroy(pos2);
                await Task.Delay(1);
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
