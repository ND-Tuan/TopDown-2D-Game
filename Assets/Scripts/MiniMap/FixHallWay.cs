using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixHallWay : MonoBehaviour
{
    public FixRoom fixRoom;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Checker")){
           fixRoom = other.GetComponent<FixRoom>();
           Invoke(nameof(SelfDes), 0.3f);
        }
        
    }

    void SelfDes(){
        if(fixRoom.NotNeedFix == false) 
            Destroy(gameObject);
       
    }

}
