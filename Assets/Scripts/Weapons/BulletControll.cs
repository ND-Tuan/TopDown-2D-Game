using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    public int Dmg;
    public bool DesByTrigger = true;
    private int DmgOutput;
    private bool IsCrit = false;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Wall") || other.CompareTag("Enemy")){ //kiểm tra đối tượng va chạm
            if (DesByTrigger) Destroy(gameObject); //hủy vật thể nếu vật thể bị hủy khi va chạm
            if( other.CompareTag("Enemy")){

                //Tham chiểu, xét bạo kích của nhân vật
                WeaponHolder weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
                DmgOutput = weaponHolder.CritChange(Dmg);

                //kiểm tra bạo kích
                if(DmgOutput > Dmg){
                    IsCrit = true;
                }

                //tham chiểu, gọi hàm gây sát thương
                other.GetComponent<EnemyControll>().TakeDmg(DmgOutput, IsCrit, 0.15f);
                IsCrit = false;
            }
        }
        
        
    }
}
