using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public GameObject POS;
    public GameObject InterFace;
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
        CDTmp = CD;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null){
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

            CDTmp -= Time.deltaTime;

            RotationToPlayer();

            if (distanceToPlayer <= Range && CDTmp <=0 && enemyAI.freeze <=0){
                CDTmp = CD; 
                animator.SetBool("Attack", true);
                Invoke(nameof(InsBullet), 0.34f);
                Invoke(nameof(delay), 0.5f);

                
            }
        }
        
    }

    void delay(){
        animator.SetBool("Attack", false);
    }

    void InsBullet(){
        GetComponent<AudioSource>().Play();
        foreach(GameObject p in FirePos){
            GameObject Tmp = Instantiate(Bullet, p.transform.position, p.transform.rotation);
            Rigidbody2D rb = Tmp.GetComponent<Rigidbody2D>();
            rb.AddForce(p.transform.right * BulletForce, ForceMode2D.Impulse);
        }
    }

    void RotationToPlayer(){
        Vector3 playerPos = Player.transform.position;
        Vector2 lookDir = playerPos - POS.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        POS.transform.rotation = rotation;

        if ( POS.transform.eulerAngles.z > 90 &&  POS.transform.eulerAngles.z < 270) 
            InterFace.transform.localScale = new Vector3(-1, 1, 0);
        else 
            InterFace.transform.localScale = new Vector3(1, 1, 0);
    }

}
