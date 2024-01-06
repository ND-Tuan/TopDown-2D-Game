using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    // Tham chiếu đến đối tượng CallMenu
    private CallMenu callMenu;

    // Mảng chứa danh sách ID của các item
    private int[] ItemsList;

    // Mảng chứa giá của các item
    private int[] Price;

    // Tham chiếu đến đối tượng RoomTemplates
    public RoomTemplates roomTemplates;

    void Start()
    {
        // Lấy tham chiếu đến đối tượng CallMenu từ tag "Menu"
        callMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>();

        // Lấy tham chiếu đến đối tượng RoomTemplates từ tag "Rooms"
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        // Khởi tạo mảng giá trị cho Price
        Price = new int[] { 30, 30, 40, 60, 60, 70, 100, 100, 110 };

        // Khởi tạo mảng giá trị cho ItemsList với các ID ngẫu nhiên
        ItemsList = new int[] { Random.Range(0, 3), Random.Range(3, 6), Random.Range(6, 9) };
    }

    void Update()
    {
        // Kiểm tra xem đối tượng Player có tồn tại không
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Tính khoảng cách đến Player
            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            // Nếu phím R được nhấn và khoảng cách đến Player nhỏ hơn 10
            if (Input.GetKeyDown(KeyCode.R) && distanceToPlayer < 10)
            {
                // Hiển thị menu cửa hàng và đặt item cho cửa hàng
                callMenu.DisplayShopMenu(true);

                // Đặt thông tin cho các item trong cửa hàng
                for (int i = 0; i < 3; i++)
                {
                    // Gọi phương thức SetItemForShop từ callMenu để thiết lập thông tin cho từng item
                    callMenu.SetItemForShop(ItemsList[i], i, Price[ItemsList[i]], roomTemplates.Poison[ItemsList[i]], true);
                }
            }
        }
    }
}
