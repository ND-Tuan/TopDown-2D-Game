using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class CheckAwaken : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyAI enemyAI;
    public Seeker seeker;
    public GameObject InterFace;
    public Animator animator;
    public EnemyControll enemyControll;
    public GolemBehaviour behaviour;
    private bool Awaken = false;
    public RoomTemplates roomTemplates;
    
    void Start(){
       
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        roomTemplates.countEnemy--;
        
        enemyControll.HealthBar.transform.localScale = new Vector3(0, 0, 0);
        enemyControll.Immune = true;
        behaviour = enemyControll.Main.GetComponent<GolemBehaviour>();
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !Awaken){
            roomTemplates.isSpawnWall = true;
            roomTemplates.countEnemy++;
            animator = InterFace.GetComponent<Animator>();
            animator.SetBool("Awaken", true);
            Invoke(nameof(GetReady), 1.5f);
        }
    }

    void GetReady(){
        enemyControll.HealthBar.transform.localScale = new Vector3(4, (float)1.6, 1);
        animator.SetBool("Awaken", false);
        enemyAI = GetComponentInParent<EnemyAI>();
        seeker = GetComponentInParent<Seeker>();

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute = true;
        GetComponentInParent<AudioSource>().mute = false;

        enemyAI.seeker = seeker;
        enemyControll.Immune = false;
        Awaken = true;
        behaviour.Active();
        Destroy(gameObject);
        
    }
}
