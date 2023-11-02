using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public GameObject[] Weapons;
    public GameObject defWe;
    private GameObject CurWeapon;
    public UnityEngine.UI.Image WeaponIcon;


    // Start is called before the first frame update
    void Start()
    {
       CurWeapon =  Instantiate(defWe, gameObject.transform, worldPositionStays:false);
       WeaponIcon.sprite =CurWeapon.GetComponent<SpriteRenderer>().sprite;
       WeaponIcon.SetNativeSize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            Destroy(CurWeapon);

        }
    }
}
