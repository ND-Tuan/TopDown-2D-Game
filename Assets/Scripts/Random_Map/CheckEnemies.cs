using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemies : MonoBehaviour
{
    public bool isClearEnemy = false;
    public int countEnemy =0;
    public bool isSpawnWall = false;
    public GameObject Clear;
    public GameObject Cam;
  
   void Update(){
        if(isSpawnWall && countEnemy == 0){
            Instantiate(Clear, Cam.transform);
            GameObject column =  GameObject.FindGameObjectWithTag("Column");
            Destroy(column);
        } 
   }
}
