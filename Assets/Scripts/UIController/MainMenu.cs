using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public GameObject BG;
    public GameObject PlayButton;
    private Animator animator;
    
    void Start(){
        Time.timeScale =1;
        animator = BG.GetComponent<Animator>();
      
    }
    public void Play(){
        animator.SetBool("Open", true);
        BG.GetComponent<AudioSource>().Play();
        PlayButton.SetActive(false);
        Invoke(nameof(LoadScene), 0.8f);
    }

    void LoadScene(){
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit(){
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
