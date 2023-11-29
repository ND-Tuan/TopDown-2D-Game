using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithController : MonoBehaviour
{
    private CallMenu callMenu;
    private List<int> ItemsList = new List<int>(); 
    private int[] Price;
    public RoomTemplates roomTemplates;
    

    void Start(){
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Price = new int[] {90,80,120,200,300};

        int number;
        number = Random.Range(0,5);
        ItemsList.Add(number);
        for(int i=0; i<2; i++){
            do{
                number = Random.Range(0,5);
            } while (ItemsList.Contains(number));
            ItemsList.Add(number);
        }
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if(Input.GetKeyDown(KeyCode.R) && distanceToPlayer<10){
            callMenu.DisplayShopMenu(true);
            for(int i=0; i<3; i++){
                callMenu.SetItemForShop(ItemsList[i], i, Price[ItemsList[i]], roomTemplates.WeaponsList[ItemsList[i]], false);
            }
        } 
    }
}
