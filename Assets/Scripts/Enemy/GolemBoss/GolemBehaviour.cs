using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehaviour : MonoBehaviour
{
    public Animator animator;
    public GameObject Attack1;
    public Transform Attack1Pos;
    public GameObject Attack2Pos;
    public GameObject Laser;
    public GameObject Attack3Pos;
    public GameObject GolemArm;
    public float GolemArmForce;
    public EnemyAI enemyAI;
    public EnemyControll enemyControll;
    public GameObject InterFace;
    private float Tmp;
    private bool CastShield= false;
    private bool TurnShield= false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
        Tmp = enemyAI.moveSpeed;
    }

    void Update(){
        if(enemyControll.EnemyCurHp <= enemyControll.EnemyMaxHp/2 && !TurnShield){
            enemyAI.moveSpeed = 0;
            CastShield= true;
            TurnShield=true;
            enemyControll.EnemyCurHp--;
            enemyControll.Immune = true;
            animator.SetBool("Shield", true);
            Invoke(nameof(ActiveShield), 1.5f);
        }

        
    }

    // Update is called once per frame
    public void Active(){
        InvokeRepeating(nameof(StartAttack), 2f, 2f);
    }
    

    void StartAttack(){
        if(!CastShield){
            int Rand = Random.Range(0,3);
            if(Rand == 0 ){
                animator.SetBool("Attack1", true);
                Invoke(nameof(ActiveAttack1), 0.75f);
            } else if (Rand == 1 ){
                animator.SetBool("Attack2", true);
                enemyAI.moveSpeed = 0;
                Invoke(nameof(ActiveAttack2), 0.5f);
            } else if(Rand == 2){
                animator.SetBool("Attack3", true);
                Invoke(nameof(ActiveAttack3), 1f);
                enemyAI.moveSpeed = 0;
            }
        }
    }

    void ActiveAttack1(){
        Instantiate(Attack1, Attack1Pos.transform.position, transform.rotation);
        animator.SetBool("Attack1", false);
    }

    void ActiveAttack2(){
        Instantiate(Laser, Attack2Pos.transform, worldPositionStays:false);
        Invoke(nameof(ReSpeed), 2f);
    }

     void ActiveAttack3(){

        // //Quay hướng bắn về phía người chơi
        // Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        // Vector2 lookDir = playerPos - Attack3Pos.transform.position;
        // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // Quaternion rotation = Quaternion.Euler(0, 0, angle);
        // Attack3Pos.transform.rotation = rotation;

        
        
        if(enemyAI.InterFace.transform.localScale == new Vector3(-1, 1, 0)) {
            Quaternion rotation = Quaternion.Euler(0, 0, 180);
            Attack3Pos.transform.rotation = rotation;
        } else{
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            Attack3Pos.transform.rotation = rotation;
        }

        GameObject BulletTmp = Instantiate(GolemArm, Attack3Pos.transform.position, Attack3Pos.transform.rotation);
        Rigidbody2D rb = BulletTmp.GetComponent<Rigidbody2D>();
        
        if(enemyAI.InterFace.transform.localScale == new Vector3(-1, 1, 0)) 
            BulletTmp.transform.localScale = new Vector3(1,-1, 0);

        rb.AddForce(Attack3Pos.transform.right * GolemArmForce, ForceMode2D.Impulse);


        animator.SetBool("Attack3", false);
        ReSpeed();

     }

    void ActiveShield(){
        enemyControll.EnemyCurShield = enemyControll.EnemyMaxShield;
        enemyControll.Immune = false;
        enemyControll.Shield.transform.localScale = new Vector3((float)1.2, (float)1.6, 0);
        animator.SetBool("Shield", false);
        CastShield= false;
        ReSpeed();
    }

    void ReSpeed(){
        enemyAI.moveSpeed = Tmp;
        animator.SetBool("Attack2", false);
        
    }
}
