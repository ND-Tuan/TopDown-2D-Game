using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHall : MonoBehaviour
{
    void Start(){
        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0, 0, 0, 0);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            gameObject.GetComponent<SpriteRenderer>().material.color =  Color.green; 
        }
    }
}
