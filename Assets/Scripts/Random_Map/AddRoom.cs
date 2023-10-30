using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

	private RoomTemplates templates;
	public GameObject Spawner;

	void Start(){
		
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.rooms.Add(this.gameObject);

		Instantiate(Spawner,transform.position, Quaternion.identity);

		// int Rand =  Random.Range(0, 5);
		// if(Rand == 0 || Rand == 1 || Rand ==2)
		// 	Instantiate(templates.Interior[Rand],transform.position, Quaternion.identity);
	}
}
