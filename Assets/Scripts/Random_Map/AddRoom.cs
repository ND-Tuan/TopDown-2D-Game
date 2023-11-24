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

		int Rand =  Random.Range(0, 10);
		if(Rand == 0 || Rand == 1 || Rand ==2)
			InteriorTmp = Instantiate(templates.Interior[Rand],transform.position, Quaternion.identity);
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
