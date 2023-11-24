using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CloseRoom : MonoBehaviour
{

  public GameObject Column;
  public GameObject[] Enemy;
  public GameObject[] closePos;
  public Transform spawnerPoint;
  public GameObject[] spawners;
  public GameObject check;
  public GameObject warning;
  public GameObject appear;
  public RoomTemplates roomTemplates;
  private int x, y ;
  public int MonsterNum;
  public bool IsSpawnEnemies = true;
  private bool isSpawn = false;
  void Start(){
		roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();	
  }


  void OnTriggerEnter2D(Collider2D other){

      if(other.CompareTag("Player") && isSpawn == false){
        
        isSpawn = true;
        foreach (GameObject pos in closePos){
          if(pos != null)
            Instantiate(Column, pos.transform.position, Quaternion.identity);
        }
        AstarPath.active.Scan();
        
        TakeRandomLocate();
        Invoke(nameof(SpawnEnemies), 0.67f);
      }   
	}

  void TakeRandomLocate(){
    // lấy vị trí ngẫu nhiên
    for(int i =0; i<MonsterNum; i++){
      x = Random.Range(-50, 50);
      y = Random.Range(-50, 50);
      spawners[i].transform.position  = spawnerPoint.position + new Vector3(x, y, 0);  
    }

    // hoạt ảnh
    foreach (GameObject pos in spawners){
      Instantiate(warning, pos.transform.position, Quaternion.identity);
      Instantiate(appear, pos.transform.position, Quaternion.identity);
    }
  }

  void SpawnEnemies(){
    if(IsSpawnEnemies==true){
        foreach (GameObject pos in spawners){
          Instantiate(Enemy[Random.Range(0, 2)], pos.transform.position, Quaternion.identity);
        }
    }
    Invoke(nameof(SpawnWall),0.01f);
    
  }

  void SpawnWall(){
      roomTemplates.isSpawnWall = true;
      roomTemplates.transform.position = spawners[0].transform.position;
      Destroy(check);
  }
}
