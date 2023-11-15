using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public GameObject POS;
    public List<GameObject>  FirePos;
    public GameObject Bullet;
    public EnemyAI enemyAI;
    public float CD;
    public float Range;
    public float BulletForce ;
    public GameObject Player ;
    private Animator animator;
    private float CDTmp;

   

    // Start is called before the first frame update
    void Start()
    {
        Player= GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        CDTmp -= Time.deltaTime;

        
        if (distanceToPlayer <= Range && CDTmp <=0){
            CDTmp = CD; 
            animator.SetBool("Attack", true);
            Invoke(nameof(InsBullet), 0.34f);
            Invoke(nameof(delay), 0.5f);
            enemyAI.roaming = false;
        }
    }

    void delay(){
        animator.SetBool("Attack", false);
    }

    void InsBullet(){
        RotationToPlayer();
        
        foreach(GameObject p in FirePos){
            GameObject Tmp = Instantiate(Bullet, p.transform.position, p.transform.rotation);
            Rigidbody2D rb = Tmp.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
        }
        enemyAI.roaming= true;
    }

    void RotationToPlayer(){
        Vector3 playerPos = Player.transform.position;
        Vector2 lookDir = playerPos - POS.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        POS.transform.rotation = rotation;
    }
}
