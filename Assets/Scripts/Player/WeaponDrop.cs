using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    private RoomTemplates templates;
    private WeaponHolder weaponHolder;
    public int WeaponId;
    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();

        gameObject.GetComponent<SpriteRenderer>().sprite = templates.WeaponsList[WeaponId].GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().spriteSortPoint= SpriteSortPoint.Center;
    }

    void  OnTriggerStay2D (Collider2D other){
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.R)){
            weaponHolder.DropWeapon(WeaponId);
            Destroy(gameObject);
        }
    }

}
