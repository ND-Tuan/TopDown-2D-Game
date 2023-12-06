using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenuControll : MonoBehaviour
{
    private CallMenu callMenu;
    public Text MaxHp;
    public Text MaxMp;
    public Text CritRate;
    public Text CritDmg;

    // Start is called before the first frame update
    void Start()
    {
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P) ){
            UnPause();
        }
    }

    public void UnPause(){
        callMenu.BGPanel.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart(){
        UnPause();
        callMenu.DestroyToReset();
        SceneManager.LoadSceneAsync(1);
        Destroy(callMenu.ObjectsDestroyToReset[0],1);
    }
    public void Quit(){
        UnPause();
        callMenu.DestroyToReset();
        SceneManager.LoadSceneAsync(0);
        Destroy(callMenu.ObjectsDestroyToReset[0],1);
    }
}
