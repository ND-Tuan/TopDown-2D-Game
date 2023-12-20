using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject DeadAnimaton;
    public GameObject Infor;
    public Text time;
    public Text Floor;
    private CallMenu callMenu;

    // Start is called before the first frame update
    void Start()
    {
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
    }

    public void BackHome(){
        
        callMenu.DestroyToReset();
        SceneManager.LoadSceneAsync(0);
        Destroy(callMenu.ObjectsDestroyToReset[0],1);
    }

    void Active(){
        Infor.SetActive(true);
        RoomTemplates roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        time.text = callMenu.UpdateLevelTimer(callMenu.TotalTime);
        Floor.text = "Floor "+ roomTemplates.Level;
    }
}
