using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRoom : MonoBehaviour
{
    public int direction;
    public Transform pos;
    public bool NotNeedFix = false;
    public RoomTemplates roomTemplates;

    void Start(){
        Destroy(gameObject, 4f);
		roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();	
		Invoke(nameof(Fix), 0.2f);
    }

    void Fix(){  
        if(NotNeedFix == false){
            Instantiate(roomTemplates.closeRooms[direction], pos.position, Quaternion.identity);
        }
       
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Checker")){
           
            NotNeedFix = true;
        }
        
    }
}
 