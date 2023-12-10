using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomAttack : MonoBehaviour
{
    private GameObject Player;
    private Animator animator;
    public EnemyAI enemyAI;
    public GameObject Explosion;
    public float Range;
    public int Dmg;
    private bool Boom = false;
    
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

        if (distanceToPlayer <= Range && !Boom){
            enemyAI.gameObject.transform.position = Player.transform.position;
            enemyAI.moveSpeed+=40;
            Boom = true;
            //enemyAI.freeze = 0.6f;
            animator.SetBool("Boom", true);
            Invoke(nameof(InsSmoke), 1.5f);
        }

        if(GetComponentInParent<EnemyControll>().EnemyCurHp<=0){
            InsSmoke();
        }
    }

    void InsSmoke(){
        GameObject tmp = Instantiate(Explosion, transform.position, Explosion.transform.rotation);
        tmp.GetComponent<DmgToPlayer>().Dmg = Dmg;
        GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>().countEnemy--;
        Destroy(enemyAI.gameObject);
    }

}
