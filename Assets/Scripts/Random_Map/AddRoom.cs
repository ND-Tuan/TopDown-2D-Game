using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

	private RoomTemplates templates;
	public GameObject Spawner;
	private GameObject SpawnerTmp;
	private GameObject InteriorTmp;
	

	void Start(){
		
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.rooms.Add(this.gameObject);

		SpawnerTmp = Instantiate(Spawner,transform.position, Quaternion.identity);

		if(Random.Range(1,11)<=4)
			InteriorTmp = Instantiate(templates.Interior[Random.Range(0,8)],transform.position, Quaternion.identity);
	}

	public void CancerSpawneEnemies(){
		for(int i =0; i<4; i++){
			SpawnerTmp.GetComponentsInChildren<CloseRoom>()[i].IsSpawnEnemies = false;
		}
		
	}

	public void ChangeToFunctionRoom(){
		Destroy(SpawnerTmp);
		Destroy(InteriorTmp);
	}
}
