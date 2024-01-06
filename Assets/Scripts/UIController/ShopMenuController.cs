using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    // Mảng chứa biểu tượng của các mục hàng trong cửa hàng
    public Image[] ItemIcons;

    // Mảng chứa giá của từng mục hàng
    public Text[] Price;

    // Mảng chứa ID của từng mục hàng
    public int[] ItemsID;

    // Đối tượng WeaponDrop để tạo vũ khí
    public GameObject WeaponDrop;

    // Biến kiểm tra xem mục hàng có phải là độc hại không
    public bool IsPoison;

    // Text hiển thị số Coin hiện tại của người chơi
    public Text CurCoin;

    // Biến lưu trữ số Coin hiện tại
    private int CurCoinInt;

    // Biến lưu trữ mục hàng được chọn
    private int CurChoose = -1;

    // Đối tượng hiển thị cửa hàng vũ khí
    public GameObject WeaponShop;

    // Đối tượng hiển thị cửa hàng độc hại
    public GameObject PoisonShop;

    // Update is called once per frame
    void Update()
    {
        // Cập nhật số Coin hiện tại từ Player
        CurCoinInt = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>().CurCoin;
        CurCoin.text = CurCoinInt.ToString();
    }

    // Phương thức chọn mục hàng
    public void Choose0(int i)
    {
        // Lưu trữ vị trí của mục hàng được chọn
        CurChoose = i;

        // Kiểm tra xem người chơi có đủ Coin để mua không
        if (CurCoinInt < int.Parse(Price[CurChoose].text))
        {
            CurCoin.color = Color.red; // Nếu không đủ, thay đổi màu văn bản sang đỏ
        }
        else
        {
            // Nếu đủ, giữ màu văn bản mặc định
            CurCoin.color = new Color(0.03137255f, 0.572549f, 0.4196079f);
        }
    }

    // Phương thức mua mục hàng
    public void Buy()
    {
        // Kiểm tra xem người chơi đã chọn mục hàng chưa và có đủ Coin không
        if (CurChoose >= 0 && CurCoinInt >= int.Parse(Price[CurChoose].text))
        {
            RoomTemplates roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();

            // Nếu mục hàng là thuốc
            if (IsPoison)
            {
                // Tạo đối tượng thuốc và di chuyển nó đến vị trí ngẫu nhiên
                GameObject tmp = Instantiate(roomTemplates.Poison[ItemsID[CurChoose]], transform.position, Quaternion.identity);
                tmp.transform.position += new Vector3(Random.Range(-5, 6), Random.Range(-2, 3) - 10, 0);
            }
            else
            {
                // Nếu không phải là thuốc, tạo đối tượng WeaponDrop và di chuyển nó đến vị trí ngẫu nhiên
                GameObject TmpW = Instantiate(WeaponDrop, transform.position, Quaternion.identity);
                TmpW.GetComponent<WeaponDrop>().WeaponId = ItemsID[CurChoose];
                TmpW.transform.position += new Vector3(Random.Range(-5, 6), Random.Range(-2, 3) - 15, 0);
            }

            // Giảm số Coin của người chơi
            player.CurCoin -= int.Parse(Price[CurChoose].text);
        }
    }
}
