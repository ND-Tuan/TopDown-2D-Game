using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBloom : MonoBehaviour
{
    public GameObject Attack;
    public GameObject firePos;
    private RoomTemplates roomTemplates;
    public GameObject Main;


    void Start()
    {
        InvokeRepeating(nameof(ActiveAttack), 1f, 4f);
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        roomTemplates.countEnemy--;
    }

    void Update(){
        if(roomTemplates.countEnemy == 0){
            Destroy(Main);
        }
    }

    void ActiveAttack(){
        Instantiate(Attack, firePos.transform.position, firePos.transform.rotation);
    }

}
