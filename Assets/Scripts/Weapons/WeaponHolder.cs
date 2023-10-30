using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public GameObject defWe;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(defWe, gameObject.transform, worldPositionStays:false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
