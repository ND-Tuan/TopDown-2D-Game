using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Staff : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject muzzle;
    public Transform firePos;
    public float TimeBtwFire = 0.2f;
    public float BulletForce;

    private float timeBtwFire;
    // Start is called before the first frame update
    void Update()
    {
        Rotate();

        timeBtwFire -= Time.deltaTime;
        if(Input.GetMouseButton(0) && timeBtwFire <0){

            Fire();
        }
    }

    // Update is called once per frame
   void Rotate()
    {
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

     void Fire(){
        timeBtwFire = TimeBtwFire;

        // GameObject BulletTmp = Instantiate(fireBall, firePos.transform.position, gameObject.transform.rotation);
        // Rigidbody2D rb = BulletTmp.GetComponent<Rigidbody2D>();
        // rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);

        GameObject BulletTmp = Instantiate(fireBall, firePos.transform, worldPositionStays:false);
        //BulletTmp.GetComponent<LaserControll>().IsEnemyBullet = false;
        
        

    }
}
