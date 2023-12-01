using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;
	public GameObject[] closeRooms;
	public GameObject[] Interior;
	public GameObject[] SPRoom;
	public GameObject Player;
	public GameObject entryRoom;
	public List<GameObject> rooms;
	public List<GameObject> Column;
	public List<GameObject> WeaponsList;
	public GameObject[] Poison;
	public float waitTime;
	public bool spawnedBoss;
	public GameObject boss;
    public int rd;
	public int Level;
	public int countEnemy =0;
    public bool isSpawnWall = false;
    public GameObject Clear;
    public GameObject Cam;
	public GameObject column;
	public GameObject WoodenChest;
	public Vector3 CentrePos;
	
	void Start(){
		Invoke(nameof(Scan), 1f);
		GameObject.FindGameObjectWithTag("PlayerPos").transform.position = new Vector3(0,0,0);
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update(){
		rd =  Random.Range(0,2);
		RemoveWall();

		if(waitTime <= 0 && spawnedBoss == false){
			if(Level==3){
				Instantiate(boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
				rooms[rooms.Count-1].GetComponent<AddRoom>().CancerSpawneEnemies();
			} else {
				rooms[rooms.Count-1].GetComponent<AddRoom>().ChangeToFunctionRoom();
				GameObject Tmp = Instantiate(SPRoom[0], rooms[rooms.Count-1].transform.position, Quaternion.identity);
				Tmp.GetComponentInChildren<PortalController>().Level = Level+1;
			}

			int rand = Random.Range(1, rooms.Count-1);
			rooms[rand].GetComponent<AddRoom>().ChangeToFunctionRoom();
			Instantiate(SPRoom[Random.Range(1,3)], rooms[rand].transform.position, Quaternion.identity);
			
			spawnedBoss = true;
		} else {
			waitTime -= Time.deltaTime;
		}
	}
	
	void Scan(){
		AstarPath.active.Scan();
	}

	void RemoveWall(){
		if(isSpawnWall && countEnemy == 0){
			isSpawnWall = false;
			
            Instantiate(Clear, Cam.transform, worldPositionStays:false);
			Instantiate(WoodenChest, gameObject.transform.position, WoodenChest.transform.rotation);
			foreach(GameObject col in Column){
				Destroy(col);
			}
			Column.Clear();
        } 
	}

	void DeSpawnWall(){
      isSpawnWall = true;
  	}
	
}
