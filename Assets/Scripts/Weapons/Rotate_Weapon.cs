using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Weapon : MonoBehaviour
{
    public bool IsFlip = true;


    // Update is called once per frame
    void Update(){
        Rotate();
    }

    void Rotate(){
        if(Camera.main != null){
            //lấy vị trí con trỏ chuột trên màn hình
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //tính vector kết nối từ vị trí vật thể đến vị trí con trỏ chuột
            Vector2 lookDir = mousePos - transform.position;

            //tính góc vector
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

            //quay vật thể về hướng con trỏ chuột
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;

            if(!IsFlip)
                if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270 ) {
                    transform.localScale = new Vector3(1, -1, 0);
                }else {
                    transform.localScale = new Vector3(1, 1, 0);
                }

            
        }
    }
}
