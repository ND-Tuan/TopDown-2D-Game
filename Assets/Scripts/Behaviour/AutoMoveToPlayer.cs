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

    void Start(){
        player= GameObject.FindGameObjectWithTag("Player");
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
    }

    void Update()
    {
        // Check if the player is close enough
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= proximityThreshold)
        {
            // Move the sprite towards the player
            Vector3 direction = player.transform.position - transform.position;
            transform.position += moveSpeed * Time.deltaTime * direction.normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            weaponHolder.AddMana();
            Destroy(gameObject);
        }
    }
}