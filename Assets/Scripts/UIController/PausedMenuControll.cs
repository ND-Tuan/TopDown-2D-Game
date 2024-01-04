using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenuControll : MonoBehaviour
{
    // Tham chiếu đến đối tượng CallMenu
    private CallMenu callMenu;

    // Hiển thị giá trị Max HP
    public Text MaxHp;

    // Hiển thị giá trị Max MP
    public Text MaxMp;

    // Hiển thị tỷ lệ Crit
    public Text CritRate;

    // Hiển thị Damage Crit
    public Text CritDmg;

    void Start()
    {
        // Lấy tham chiếu đến đối tượng CallMenu từ tag "Menu"
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
    }

    void Update()
    {
        // Nếu phím P được nhấn, gọi phương thức UnPause()
        if (Input.GetKeyDown(KeyCode.P))
        {
            UnPause();
        }
    }

    // Phương thức hủy tạm dừng game
    public void UnPause()
    {
        // Ẩn BGPanel của CallMenu
        callMenu.BGPanel.SetActive(false);

        // Ẩn đối tượng PausedMenuControl
        gameObject.SetActive(false);

        // Đặt lại tốc độ thời gian về 1
        Time.timeScale = 1;
    }

    // Phương thức restart game
    public void Restart()
    {
        // Gọi phương thức UnPause() để tiếp tục game
        UnPause();

        // Gọi phương thức DestroyToReset() từ đối tượng callMenu
        callMenu.DestroyToReset();

        // Chuyển đến scene có index là 1 (nơi bắt đầu game)
        SceneManager.LoadSceneAsync(1);

        // Hủy đối tượng được đánh dấu để hủy sau 1 giây
        Destroy(callMenu.ObjectsDestroyToReset[0], 1);
    }

    // Phương thức thoát game và quay về menu chính
    public void Quit()
    {
        // Gọi phương thức UnPause() 
        UnPause();

        // Gọi phương thức DestroyToReset() từ đối tượng callMenu
        callMenu.DestroyToReset();

        // Chuyển đến scene có index là 0 (menu chính)
        SceneManager.LoadSceneAsync(0);

        // Hủy đối tượng được đánh dấu sau 1 giây
        Destroy(callMenu.ObjectsDestroyToReset[0], 1);
    }
}
