using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemArm : MonoBehaviour
{
    public float sec;
    public GameObject Arm;

    // Start is called before the first frame update
    void Start(){

        Invoke(nameof(Stay), sec);
        Invoke(nameof(SelfDes), sec);
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Wall")){
            Stay();
            SelfDes();
        }
    }

     void SelfDes(){

        Destroy(gameObject);
    }

    void Stay(){
        GameObject GolemArm = Instantiate(Arm, transform.position, transform.rotation);
        if(transform.localScale == new Vector3(1,-1, 0))
            GolemArm.transform.localScale = new Vector3(1,-1, 0);
    }
}
