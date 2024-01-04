using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Đối tượng chứa background
    public GameObject BG;

    // Đối tượng nút Play
    public GameObject PlayButton;

    // Animator của background
    private Animator animator;

    void Start()
    {
        // Đặt tốc độ thời gian về 1 khi bắt đầu scene
        Time.timeScale = 1;

        // Lấy tham chiếu đến Animator của đối tượng BG
        animator = BG.GetComponent<Animator>();
    }

    // Phương thức chạy khi nút Play được nhấn
    public void Play()
    {
        // Kích hoạt animation mở cửa sổ
        animator.SetBool("Open", true);

        // Phát âm thanh của đối tượng BG
        BG.GetComponent<AudioSource>().Play();

        // Ẩn nút Play
        PlayButton.SetActive(false);

        // Gọi phương thức LoadScene() sau 0.8 giây
        Invoke(nameof(LoadScene), 0.8f);
    }

    // Phương thức load scene mới
    void LoadScene()
    {
        // Chuyển đến scene có index là 1
        SceneManager.LoadSceneAsync(1);
    }

    // Phương thức thoát game
    public void Quit()
    {
        // Thoát ứng dụng
        Application.Quit();

        // Dừng chạy trong trình chỉnh sửa Unity
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
