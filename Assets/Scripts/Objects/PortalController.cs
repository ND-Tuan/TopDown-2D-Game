using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public int Level;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>().ChangeSceneEffect();
            Invoke(nameof(Function), 0.77f);
        }
    }

    void Function(){
        SceneManager.LoadSceneAsync(Level);
    }


}
