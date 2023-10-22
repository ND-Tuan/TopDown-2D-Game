using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public Animator animator;
     public Rigidbody2D rb;

    // Start is called before the first frame update

    void Start(){
        animator = GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Wall") || other.CompareTag("Enemy")){
            animator.SetBool("Hit", true);
            rb.velocity = new Vector3(0,0,0);
            Invoke(nameof(SelfDes),0.5f);
        }
        
    }

    void SelfDes(){
        Destroy(gameObject);
    }
}
