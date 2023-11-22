using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float rotZ;
    public bool ClockWise;
    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        if(ClockWise){

            rotZ += Time.deltaTime * RotationSpeed;

        } else {

            rotZ += -Time.deltaTime * RotationSpeed;

        }
        
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

    }
}
