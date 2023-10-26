using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static float DashCDTmp;
    public GameObject DashAnimation;
    public Transform DashPos;
    public SpriteRenderer  CharacterSR;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    void Update(){
       
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveSpeed * Time.deltaTime * moveInput; 

        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (moveInput.x != 0)
            if (moveInput.x < 0)
                CharacterSR.transform.localScale = new Vector3(-1, 1, 0);
            else
                CharacterSR.transform.localScale = new Vector3(1, 1, 0); 

        DashCDTmp -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0 && DashCDTmp<0)
        {
            DashCDTmp = DashCD;
            animator.SetBool("Dash", false);

            moveSpeed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once)
        {
            animator.SetBool("Dash", false);
            moveSpeed -= dashBoost;
            once = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }  

    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("EnemyBullet")) {
            
            CharacterSR.GetComponent<SpriteRenderer>().material.color = Color.red;
            Invoke(nameof(Nomal), 0.1f);
        }
        
    }

    void Nomal(){
       CharacterSR.GetComponent<SpriteRenderer>().material.color = Color.white;
    }

}
