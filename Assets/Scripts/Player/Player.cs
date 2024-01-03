using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D Rg;
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
    public GameObject Attack;
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
    public int EXP=0;
    public Text Coin;
    public Text ExpText;
    private bool InChargeTime = false;
    private bool ChangeToHand =  false;
    private float HandCD =0;
    public GameObject VRCam;
    private CinemachineVirtualCamera virtualCamera;
    public GameObject DmgPopup;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        WeaponPos =GameObject.FindGameObjectWithTag("WeaponPos");
        virtualCamera =  VRCam.GetComponent<CinemachineVirtualCamera>();
        PlayerCurHP = PlayerMaxHP;
        Invoke(nameof(Appear), 0.5f); 
    }

    void Update(){

        //Hiển thị máu
        HpBar.maxValue = PlayerMaxHP;
        HpBar.value = PlayerCurHP;
        text.text = PlayerCurHP + "/" + PlayerMaxHP;
        Coin.text = CurCoin.ToString();
        ExpText.text = "EXP: " + EXP;

        if(PlayerCurHP <=0) {
            CallMenu callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
            callMenu.GameOver();
        }
        if(appear && Time.timeScale >0){
            if(!InChargeTime){
                Move();
                if(!InUltiTime){
                    Dash();
                    HandAttack();
                }
            }
            SkillAsync();
        }

        if(Input.GetKeyDown(KeyCode.E) && !InUltiTime){
            if(ChangeToHand == false){
                WeaponPos.GetComponent<WeaponHolder>().Rotationable = false;
                ChangeToHand = true;
                WeaponPos.GetComponent<WeaponHolder>().RemoveWeapon();
            } else {
                Attack.SetActive(false);
                animator.SetBool("Attack", false);
                ChangeToHand = false;
                WeaponPos.GetComponent<WeaponHolder>().RestoreWeapon();
            }
        }
    }

    void Move(){

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        if(moveInput.x!=0 && moveInput.y!=0) moveInput.Normalize(); //Chuẩn hóa vector khi nv di chuyển trên đường chéo
         Rg.velocity = moveSpeed * moveInput;

        //transform.position += moveSpeed * Time.deltaTime * moveInput;

        animator.SetFloat("Speed", Rg.velocity.sqrMagnitude);

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
            if(!ChangeToHand)
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

    //  Skill đặc biệt
    async void SkillAsync(){

        //cooldown
        if(!InUltiTime) {
            if(SkillCDTmp >0){
                SkillCDTmp -= Time.deltaTime;
            } else {
                SkillCDTmp=0;
            }   
        }

        //xử lý hiển thị hồi/nạp của skill
        UltiIconCD.fillAmount = 1 - SkillCDTmp/SkillCD;
        UltiIconEnergy.fillAmount = Energy;
        
        //
        if(Input.GetMouseButton(1) && SkillCDTmp==0){
            Rg.velocity = new Vector3(0,0,0);
            WeaponPos.GetComponent<WeaponHolder>().RemoveWeapon();
            animator.SetBool("Charge", true);
            InChargeTime = true;

            //nạp năng lượng
            if(Energy <1){
                if (ChargeTime<=0) ChargeTime=0.000001f;
                Energy += Time.deltaTime/ChargeTime;
                virtualCamera.m_Lens.OrthographicSize += 30*Time.deltaTime/ChargeTime; //Mở rộng ống kính

            } else {
                Energy = 1;
                virtualCamera.m_Lens.OrthographicSize = 80;
            }

        } else{
            animator.SetBool("Charge", false);
            InChargeTime = false;

            CinemachineVirtualCamera virtualCamera =  VRCam.GetComponent<CinemachineVirtualCamera>();
            
            if(Energy==1 && !InUltiTime){
                animator.SetBool("Blash", true);
                TmpKameHa = Instantiate(KameHa, ArmPos.gameObject.transform, worldPositionStays:false);
                SkillCDTmp = SkillCD;
                InUltiTime = true;
            } else if(!InUltiTime   && dashTime <= 0) {
                if(!ChangeToHand)
                    WeaponPos.GetComponent<WeaponHolder>().RestoreWeapon();

                //Mở rộng ống kính    
                while(virtualCamera.m_Lens.OrthographicSize > 50){
                   virtualCamera.m_Lens.OrthographicSize --;
                   await Task.Delay(200);
                }
                virtualCamera.m_Lens.OrthographicSize = 50;
            }
        }
    }

    //Chế độ Đánh tay (Không vũ khí)
    void HandAttack(){
        HandCD -= Time.deltaTime;
        if(Input.GetMouseButton(0) && ChangeToHand && HandCD <=0){
            
            animator.SetBool("Attack", true);
            Attack.SetActive(true);
            Attack.GetComponent<HandAttack>().Attack1();
            HandCD = 1.1f;
        }

        if(HandCD <= 0.1)  animator.SetBool("Attack", false);
    }

    void Nomal(){
       CharacterSR.GetComponent<SpriteRenderer>().material.color = Color.white;
       immune = false;
    }

    //Nhận sát thương
    public void TakeDmg(int Dmg){
        if(immune ==  false){
            PlayerCurHP-= Dmg;
            //ScreenShake(5, 200);
            if (PlayerCurHP <0) PlayerCurHP = 0;
            immune = true;
            CharacterSR.GetComponent<SpriteRenderer>().material.color = Color.red;
            Invoke(nameof(Nomal), 0.1f);
        } 
    }

    void Appear(){
        animator.SetBool("Appear", true);
        appear = true;
    }

    //Hồi máu
    public async void AddHp(int Amount){
        GameObject instance = Instantiate(DmgPopup, gameObject.transform, worldPositionStays: false);
        instance.GetComponent<TextMesh>().text ="+" +Amount;
        instance.GetComponent<TextMesh>().color = new Color(1, 0.2584905f, 0.2584905f, 1);
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
        GetComponent<AudioSource>().Play();
    }

    //Rung màn hình
    public async void ScreenShake(float Instensity, int time){
        CinemachineBasicMultiChannelPerlin multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannelPerlin.m_AmplitudeGain = Instensity;
        await Task.Delay(time);
        multiChannelPerlin.m_AmplitudeGain = 0;

    }

}
