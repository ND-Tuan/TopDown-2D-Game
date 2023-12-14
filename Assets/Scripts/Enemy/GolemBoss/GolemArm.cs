using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemArm : MonoBehaviour
{
    public float sec;
    public GameObject Arm;
    private ArmBloom armBloom;

    // Start is called before the first frame update
    void Start(){

        Invoke(nameof(Stay), sec);
        Destroy(gameObject, sec);
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Wall")){
            Stay();
             Destroy(gameObject);
        }
    }

    void Stay(){
        GameObject GolemArm = Instantiate(Arm, transform.position, Arm.transform.rotation);
        if(transform.localScale == new Vector3(1,-1, 0))
            armBloom = GolemArm.GetComponentInChildren<ArmBloom>();

            if(armBloom !=null){
                armBloom.gameObject.transform.rotation = Quaternion.Euler(0, 0, -130);
                armBloom.gameObject.transform.localScale = new Vector3(1,-1, 0);
            } 
    }
}
