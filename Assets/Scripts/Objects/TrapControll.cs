using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControll : MonoBehaviour
{
    public GameObject DmgGiver;

    void Active(){
        DmgGiver.SetActive(true);
    }

    void DeActive(){
        DmgGiver.SetActive(false);
    }
}
