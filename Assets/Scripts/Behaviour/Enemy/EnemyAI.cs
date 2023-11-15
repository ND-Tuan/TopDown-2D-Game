using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.TextCore.Text;


public class EnemyAI : MonoBehaviour
{

    public bool roaming = true;
    public float moveSpeed = 2f;
    public Animator animator;
    public SpriteRenderer  InterFace;
    private RoomTemplates roomTemplates;
    public Seeker seeker;
    public bool update;
    private bool reachPlayer = false;
    Path path;
    Coroutine moveCoroutine;
    public float nextWayPointDistance;
    public float proximityThreshold = 50f;
    public Vector2 target;
    public Vector3 playerPos;
    public float distanceToPlayer;

    private void Start(){
        InvokeRepeating(nameof(CalculatePath), 0f, 0.5f);
        reachPlayer = true;

        animator = GetComponentInChildren<Animator>();
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        
    }

    void CalculatePath(){

        target = FindTarget();

        if(seeker != null)
            if(seeker.IsDone() && (reachPlayer || update) )
                seeker.StartPath(transform.position, target, OnPathCompleted);
    }

    void OnPathCompleted(Path p){
        
        if(p.error) return;
        path = p;
        MoveToTarget();
    }

    void MoveToTarget(){

        if(moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCorouttine());
    }

    IEnumerator MoveToTargetCorouttine(){

        int currentWP = 0;
        reachPlayer = false;

        while  (currentWP < path.vectorPath.Count){

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            animator.SetFloat("Speed", direction.sqrMagnitude);

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance)
                currentWP++;

            if (force.x != 0)
                if (force.x < 0)
                    InterFace.transform.localScale = new Vector3(-1, 1, 0);
                else
                    InterFace.transform.localScale = new Vector3(1, 1, 0);

            yield return null;
        }

        reachPlayer = true;
    }

    Vector2 FindTarget(){

        
        playerPos = FindObjectOfType<Player>().transform.position;
        
        distanceToPlayer = Vector3.Distance(transform.position, playerPos);

        if (distanceToPlayer <= proximityThreshold){
            if(roaming){

                return (Vector2)playerPos + (Random.Range(10f, 50f) *new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
            } else {

                return playerPos;
            }
        } else{
            Vector3 RandPos = transform.position + new Vector3(Random.Range(-10,10),Random.Range(-10,10), 0);
            return RandPos;
        }

        
    }

}