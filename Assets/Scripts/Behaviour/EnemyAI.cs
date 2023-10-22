using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = 2f;
    public float nextWayPointDistance = 2f;
    public float repeatTimeUpdatePath = 0.5f;
    public Animator animator;
    public int EnemyMaxHp;
    private int EnemyCurHp;
    public GameObject HealthBar;
    public GameObject Death;
    public RoomTemplates roomTemplates;
   
    Path path;
    public Seeker seeker;
    private Rigidbody2D rb;

    Coroutine moveCoroutine;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        target = FindObjectOfType<Player>().transform;
        InvokeRepeating(nameof(CalculatePath), 0, repeatTimeUpdatePath);
		roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        EnemyCurHp = EnemyMaxHp;	

        roomTemplates.countEnemy ++;
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("bullet")) {
            EnemyCurHp --;
            double Heal = EnemyCurHp *(1.4/EnemyMaxHp);
            HealthBar.transform.localScale = new Vector3((float)Heal, 1, 0);
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
            Invoke(nameof(Nomal), 0.1f);
        }
        if( EnemyCurHp ==0) {
            roomTemplates.countEnemy --;
            Instantiate(Death, transform.position, Death.transform.rotation);
            Destroy(gameObject);
        }
    }

    void CalculatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, target.position, OnPathCompleted);
    }

    void OnPathCompleted(Path p)
    {
        if (p.error) return;
            path = p;
            MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count)
        {

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector3 force = direction * moveSpeed * Time.deltaTime;
            transform.position += force;

            animator.SetFloat("Speed", direction.sqrMagnitude);

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance)
                currentWP++;

            if (force.x != 0)
                if (force.x < 0)
                    transform.localScale = new Vector3(-1, 1, 0);
                else
                    transform.localScale = new Vector3(1, 1, 0);

            yield return null;
        }
    }

    void Nomal(){
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    

    
}
