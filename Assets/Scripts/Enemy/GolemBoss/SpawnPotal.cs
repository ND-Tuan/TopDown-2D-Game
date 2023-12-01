using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPotal : MonoBehaviour
{
    public GameObject Portal;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(spawnPotal), 1);
    }

    void spawnPotal(){
        RoomTemplates roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        GameObject Tmp = Instantiate(Portal, roomTemplates.rooms[roomTemplates.rooms.Count-1].transform.position, Quaternion.identity);
		Tmp.GetComponentInChildren<PortalController>().Level = roomTemplates.Level+1;
    }

    
}
