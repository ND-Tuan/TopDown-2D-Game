using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnding : MonoBehaviour
{
    private CallMenu callMenu;
    void Start()
    {
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
    }

     void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            callMenu.DisplayChange();
        }
    }

    public void  DisplayResult(){
        callMenu.DisplayResult();
    }

}
