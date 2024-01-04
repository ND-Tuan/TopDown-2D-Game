using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultMenuController : MonoBehaviour
{
    // Tham chiếu đến CallMenu để gọi các phương thức quản lý menu
    private CallMenu callMenu;

    // Mảng chứa các biểu tượng vũ khí trong giao diện kết quả
    public Image[] WeaponIcon;

    // Text hiển thị thời gian và ngày
    public Text time;
    public Text date;

    void Start()
    {
        // Kết nối với đối tượng CallMenu thông qua tag "Menu"
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
        
        // Ẩn giao diện kết thúc trận đấu
        callMenu.UnDisplayEnding();

        // Hiển thị ngày hiện tại
        date.text = DateTime.Now.ToShortDateString();

        // Kết nối và hiển thị biểu tượng vũ khí
        WeaponHolder weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();
        RoomTemplates roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        for(int i = 0; i < 2; i++)
        {
            // Lấy sprite của vũ khí từ danh sách và hiển thị
            WeaponIcon[i].sprite = roomTemplates.WeaponsList[weaponHolder.Weapons[i]].GetComponent<SpriteRenderer>().sprite;
            WeaponIcon[i].SetNativeSize(); // Đảm bảo kích thước đúng của hình ảnh
        }
    }

    // Xử lý khi nhấn nút Back Home
    public void BackHome()
    {
        // Khôi phục thời gian trôi qua trong trò chơi
        Time.timeScale = 1;

        // Hủy các đối tượng để chuẩn bị cho việc reset trò chơi
        callMenu.DestroyToReset();

        // Chuyển đến màn hình chính
        SceneManager.LoadSceneAsync(0);

        // Hủy đối tượng cần hủy sau khoảng 1 giây
        Destroy(callMenu.ObjectsDestroyToReset[0], 1);
    }
}
