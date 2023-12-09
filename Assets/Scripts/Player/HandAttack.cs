using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    public GameObject[] attack;
    
    void Rest(){
        foreach(GameObject a in attack){
            a.SetActive(false);
        }
    }

    public void Attack1(){
        attack[0].SetActive(true);
    }

    void Attack2(){
        attack[1].SetActive(true);
    }

    void Attack3(){
        attack[2].SetActive(true);
    }

    void DeActive(){
        gameObject.SetActive(false);
    }

}
