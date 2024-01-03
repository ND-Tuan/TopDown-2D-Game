using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.TextCore.Text;
using UnityEngine.PlayerLoop;


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
    private Vector2 Final;
    public float freeze;
    public float UpdateRate = 0.5f;

    private void Start(){
        InvokeRepeating(nameof(CalculatePath), 0f, UpdateRate);
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

            while (freeze > 0)
            {
                freeze -= Time.deltaTime;
                yield return null;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            animator.SetFloat("Speed", direction.sqrMagnitude);

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance)
                currentWP++;

            if (force.x != 0 )
                if(!roaming){
                    if (force.x < 0)
                        InterFace.transform.localScale = new Vector3(-1, 1, 0);
                    else
                        InterFace.transform.localScale = new Vector3(1, 1, 0);
                } else {
                    Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                    Vector2 lookDir = playerPos - transform.position;
                    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);

                    if ( rotation.eulerAngles.z > 90 &&  rotation.eulerAngles.z < 270) 
                        InterFace.transform.localScale = new Vector3(-1, 1, 0);
                    else 
                        InterFace.transform.localScale = new Vector3(1, 1, 0);
                }
            yield return null;
        }

        reachPlayer = true;
    }

    //Tìm vị trí người chơi
    Vector3 FindTarget(){

        playerPos = FindObjectOfType<Player>().transform.position;
        
        distanceToPlayer = Vector3.Distance(transform.position, playerPos);

        if (distanceToPlayer <= proximityThreshold){
            
            if(roaming){
                //lấy ngẫu nhiên vị trí xung quanh player
                Final = (Vector2)playerPos + (Random.Range(10f, 50f) *new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)).normalized);
                while(Vector3.Distance(Final, playerPos) < 15){
                    Final = (Vector2)playerPos + (Random.Range(10f, 50f) *new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)).normalized);
                }
                return Final;
            } else {

                return playerPos;
            }
        } else{
            Vector3 RandPos = transform.position + new Vector3(Random.Range(-10,10),Random.Range(-10,10), 0);
            return RandPos;
        }

        
    }

}