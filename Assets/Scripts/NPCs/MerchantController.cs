using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MerchantController : MonoBehaviour
{
    private CallMenu callMenu;
    private int[] ItemsList;
    private int[] Price;
    public RoomTemplates roomTemplates;
    

    void Start(){
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Price = new int[] {30, 30, 40, 60, 60, 70, 100, 100, 110};
        ItemsList = new int[] {Random.Range(0,3), Random.Range(3,6), Random.Range(6,9)};
    }
    
    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if(Input.GetKeyDown(KeyCode.R) && distanceToPlayer<10){
            callMenu.DisplayShopMenu(true);
            for(int i=0; i<3; i++){
                callMenu.SetItemForShop(ItemsList[i], i, Price[ItemsList[i]], roomTemplates.Poison[ItemsList[i]], true);
            }
        } 
    }
}
