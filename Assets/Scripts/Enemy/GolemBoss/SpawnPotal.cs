using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPotal : MonoBehaviour
{
    public GameObject Portal;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute = false;
        Invoke(nameof(spawnPotal), 1);
    }

    void spawnPotal(){
        RoomTemplates roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        GameObject Tmp = Instantiate(Portal, new Vector3(160,0,0), Quaternion.identity);
		Tmp.GetComponentInChildren<PortalController>().Level = roomTemplates.Level+1;
    }

    
}
