using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private GameObject Player;
    private Animator animator;
    public EnemyAI enemyAI;
    private float CDTmp;
    public GameObject Smoke;
    public Transform pos;
    public float CD;
    public float Range;
    public float Scale;
    
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
        if(Player != null && enemyAI!= null){
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

            CDTmp -= Time.deltaTime;

        

        if (distanceToPlayer <= Range && CDTmp <=0 && enemyAI.freeze <=0){
            CDTmp = CD;
            animator.SetBool("Attack", true);
            Invoke(nameof(delay), 0.41f);
            Invoke(nameof(delayro), 4f);
            Invoke(nameof(InsSmoke), 0.25f);
        }
        }
        
    }

    void delay(){
        animator.SetBool("Attack", false);
        enemyAI.roaming= true;
       
    }

    void InsSmoke(){
        GameObject tmp = Instantiate(Smoke, pos.transform.position, Smoke.transform.rotation);
        tmp.transform.localScale = new Vector3 ((float)Scale, (float)Scale, 0);
    }

    void delayro(){
        enemyAI.roaming = false;
       
    }
}
