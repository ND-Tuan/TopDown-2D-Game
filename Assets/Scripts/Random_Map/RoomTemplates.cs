using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;
	public GameObject[] closeRooms;

	public GameObject entryRoom;

	public List<GameObject> rooms;

	public List<GameObject> Column;

	public float waitTime;
	private bool spawnedBoss;
	public GameObject boss;
    public int rd;

	public int countEnemy =0;
    public bool isSpawnWall = false;
    public GameObject Clear;
    public GameObject Cam;

	public GameObject column;


	void Start(){
		Invoke(nameof(Scan), 1f);
	}

	void Update(){
		rd =  Random.Range(0,2);
		RemoveWall();
	}

	
	void Scan(){
		AstarPath.active.Scan();
	}

	void RemoveWall(){
		if(isSpawnWall && countEnemy == 0){
			isSpawnWall = false;
			waitTime =0;
            Instantiate(Clear, Cam.transform, worldPositionStays:false);
			foreach(GameObject col in Column){
				Destroy(col);
			}
			Column.Clear();
        } 
	}

	
}
