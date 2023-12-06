using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
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
        Time.timeScale = 1;
        Destroy(callMenu.ObjectsDestroyToReset[0],1);
    }
}
