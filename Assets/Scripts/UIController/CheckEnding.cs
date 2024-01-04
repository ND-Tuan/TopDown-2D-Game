using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnding : MonoBehaviour
{
    // Tham chiếu đến đối tượng CallMenu
    private CallMenu callMenu;

    void Start()
    {
        // Lấy tham chiếu đến đối tượng CallMenu từ tag "Menu"
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
    }

    // Phương thức được gọi khi Collider2D kích hoạt va chạm
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng va chạm có tag là "Player" không
        if (other.CompareTag("Player"))
        {
            // Gọi phương thức DisplayChange() từ đối tượng callMenu
            callMenu.DisplayChange();
        }
    }

    // Phương thức hiển thị kết quả
    public void DisplayResult()
    {
        // Gọi phương thức DisplayResult() từ đối tượng callMenu
        callMenu.DisplayResult();
    }
}
