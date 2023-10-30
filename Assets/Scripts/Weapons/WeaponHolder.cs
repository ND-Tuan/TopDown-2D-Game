using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public GameObject[] Weapons;
    public GameObject defWe;
    private GameObject CurWeapon;


    // Start is called before the first frame update
    void Start()
    {
       CurWeapon =  Instantiate(defWe, gameObject.transform, worldPositionStays:false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            Destroy(CurWeapon);

        }
    }
}
