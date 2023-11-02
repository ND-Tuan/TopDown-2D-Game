using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Weapon : MonoBehaviour
{

    // Update is called once per frame
    void Update(){

        Rotate();
    }

    void Rotate(){
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270) 
            transform.localScale = new Vector3(1, -1, 0);
        else 
            transform.localScale = new Vector3(1, 1, 0);

    }
}
