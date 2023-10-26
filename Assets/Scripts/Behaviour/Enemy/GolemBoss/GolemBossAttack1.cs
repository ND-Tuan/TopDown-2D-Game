using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossAttack1 : MonoBehaviour
{
     public GameObject Bullet;
     public float BulletForce;
    // Start is called before the first frame update
    void Start()
    {
       Fire();
    }

    // Update is called once per frame
   void Fire(){

        GameObject BulletTmp = Instantiate( Bullet, transform.position, gameObject.transform.rotation);
        
        Rigidbody2D rb = BulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
    }
}
