using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensionTableControll : MonoBehaviour
{
    // Tham chiếu đến đối tượng CallMenu
    private CallMenu callMenu;

    // Tham chiếu đến Animator để điều khiển animation
    public Animator animator;

    void Start()
    {
        // Lấy tham chiếu đến đối tượng CallMenu từ tag "Menu"
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();
    }

    void Update()
    {
        // Kiểm tra xem đối tượng Player có tồn tại không
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Tính khoảng cách đến Player
            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            // Nếu phím R được nhấn và khoảng cách đến Player nhỏ hơn 15
            if (Input.GetKeyDown(KeyCode.R) && distanceToPlayer < 15)
            {
                // Kích hoạt animation và gọi phương thức Active sau 1.5 giây
                animator.SetBool("Active", true);
                Invoke(nameof(Active), 1.5f);
            }
        }
    }

    // Phương thức được gọi sau khi hoàn thành animation
    public void Active()
    {
        // Hiển thị menu thăng cấp
        callMenu.DisplayAscentionMenu();

        // Tắt animation
        animator.SetBool("Active", false);
    }
}
