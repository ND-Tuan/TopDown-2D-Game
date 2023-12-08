using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensionTableControll : MonoBehaviour
{
    private CallMenu callMenu;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null){
            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if(Input.GetKeyDown(KeyCode.R) && distanceToPlayer<15){
                animator.SetBool("Active", true);
                Invoke(nameof(Active), 1.5f);
            } 
        }
    }

    public void Active(){
        callMenu.DisplayAscentionMenu();
        animator.SetBool("Active", false);
    }
}
