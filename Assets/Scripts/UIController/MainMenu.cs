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
        animator = BG.GetComponent<Animator>();
    }
    public void Play(){
        animator.SetBool("Open", true);
        PlayButton.SetActive(false);
        Invoke(nameof(LoadScene), 1f);
    }

    void LoadScene(){
        SceneManager.LoadSceneAsync(1);
    }
}
