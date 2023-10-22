using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dash : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 720f;

    public GameObject DashAnimation;
    public Transform DashPos;



    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);

        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

         if (Input.GetKeyDown(KeyCode.Space) && Player.dashTime <= 0 && Player.DashCDTmp<0){
            Instantiate(DashAnimation, DashPos.position, transform.rotation, transform);
         }
    }
}


