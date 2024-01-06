using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    // Đối tượng chứa animation khi người chơi chết
    public GameObject DeadAnimaton;

    // Đối tượng chứa thông tin hiển thị
    public GameObject Infor;

    // Hiển thị thời gian chơi
    public Text time;

    // Hiển thị số tầng
    public Text Floor;

    // Tham chiếu đến đối tượng CallMenu
    private CallMenu callMenu;

    void Start()
    {
        // Lấy tham chiếu đến đối tượng CallMenu từ tag "Menu"
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
    }

    // Phương thức Backhome
    public void BackHome()
    {
        // Gọi phương thức DestroyToReset() từ đối tượng callMenu
        callMenu.DestroyToReset();

        // Chuyển đến scene 0
        SceneManager.LoadSceneAsync(0);

        // Hủy đối tượng được đánh dấu để hủy sau 1 giây
        Destroy(callMenu.ObjectsDestroyToReset[0], 1);
    }

    // Phương thức hiển thị thông tin
    void Active()
    {
        // Hiển thị Infor
        Infor.SetActive(true);

        // Hiển thị Rooms
        RoomTemplates roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        // Hiển thị thời gian chơi và số tầng
        time.text = callMenu.UpdateLevelTimer(callMenu.TotalTime);
        Floor.text = "Floor " + roomTemplates.Level;
    }
}
