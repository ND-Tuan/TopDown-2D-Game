using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
   public GameObject can;

   void DeActive(){
        can.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
   }
}
