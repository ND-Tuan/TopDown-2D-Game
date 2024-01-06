using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{

    public int[] OpenDirection; //phòng mở hướng: 0-trái, 1-trên, 2-phải, 3-dưới
    public int rand;
    private int rd;
    public GameObject room;
    public bool has3Direction;

    private RoomTemplates roomTemplates;
    private int maxRooms=5;
    public bool isSpawn = false;

    // Start is called before the first frame update

    void Start()
    {
        Destroy(gameObject, 4f);
		roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();	
		Invoke(nameof(Spawn), 0.1f);
        maxRooms = 5+roomTemplates.Level-1;
    }

    void Spawn(){
        if(isSpawn == false && roomTemplates.rooms.Count< maxRooms){
            foreach(int i in OpenDirection){
                if(i==0){  
                    rand = Random.Range(2, roomTemplates.rightRooms.Length);
                    if((has3Direction && roomTemplates.rd ==0) || roomTemplates.rooms.Count > maxRooms) rand = 0;
                    Instantiate(roomTemplates.rightRooms[rand], transform.position, roomTemplates.rightRooms[rand].transform.rotation);
                }

                if(i==1){
                    rand = Random.Range(1, roomTemplates.bottomRooms.Length);
                    if(roomTemplates.rooms.Count > maxRooms) rand =0;
                    Instantiate(roomTemplates.bottomRooms[rand], transform.position, roomTemplates.bottomRooms[rand].transform.rotation);
                }

                if(i==2){ 
                    rand = Random.Range(2, roomTemplates.leftRooms.Length);
                    if((has3Direction && roomTemplates.rd == 1)|| roomTemplates.rooms.Count > maxRooms) rand = 0;
                    Instantiate(roomTemplates.leftRooms[rand], transform.position, roomTemplates.leftRooms[rand].transform.rotation);
                }

                if(i==3){
                    rand = Random.Range(2, roomTemplates.topRooms.Length);
                    Instantiate(roomTemplates.topRooms[rand], transform.position, roomTemplates.topRooms[rand].transform.rotation);
                }
                
                isSpawn = true;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("SpawnPoint")){
            if(other.gameObject.TryGetComponent<SpawnRoom>(out var spawnRoom))
            {
                if(spawnRoom.isSpawn == false && isSpawn == false){
                    Instantiate(roomTemplates.entryRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                } 
                isSpawn = true;
            }
                
        }
        
    }

   
}
