using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoMoveToPlayer : MonoBehaviour
{
    // Reference to the player object
    public GameObject player;
    private WeaponHolder weaponHolder;

    // Speed of the sprite's movement

    public float moveSpeed = 3f;

    // Threshold distance for triggering movement
    public float proximityThreshold = 5f;
    public bool isCoin = false;

    void Start(){
        player= GameObject.FindGameObjectWithTag("Player");
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
    }

    void Update()
    {
        if(player !=null){
            // Check if the player is close enough
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
             if (distanceToPlayer <= proximityThreshold)
        {
            // Move the sprite towards the player
            Invoke(nameof(Delay), 0.5f);
        }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            if(isCoin){
                player.GetComponentInParent<Player>().ChangeCoinAmount(1);
                player.GetComponentInParent<AudioSource>().Play();
            }else{
                weaponHolder.AddMana(1);
                weaponHolder.gameObject.GetComponentInParent<AudioSource>().Play();
            }
            Destroy(gameObject);
        }
    }

    void Delay(){
        Vector3 direction = player.transform.position - transform.position;
        transform.position += moveSpeed * Time.deltaTime * direction.normalized;
    }
}