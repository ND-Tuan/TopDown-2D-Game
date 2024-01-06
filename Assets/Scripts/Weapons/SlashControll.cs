using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashControll : MonoBehaviour
{
   public int Dmg;
   private int DmgOutput;
   private bool IsCrit = false;

   void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("Enemy")){ //kiểm tra đối tượng va chạm

            //tham chiểu kiểm tra tỉ lệ bạo kích nhân vật
            WeaponHolder weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
            DmgOutput = weaponHolder.CritChange(Dmg);

            //kiểm tra bạo kích
            if(DmgOutput > Dmg){
               IsCrit = true;
            }

            //gọi hàm gây sát thương cho kẻ thù
            other.GetComponent<EnemyControll>().TakeDmg(DmgOutput, IsCrit, 0.15f);
            IsCrit = false;
        }
        //hủy vật thể
        if(other.CompareTag("EnemyBullet")) Destroy(other.gameObject);
        
   }
}
