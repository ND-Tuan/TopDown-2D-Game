using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AscendController : MonoBehaviour
{
    // Biến lưu trữ hình ảnh Buff
    public Image Buff;

    // Text hiển thị điểm kinh nghiệm
    public Text Exp;

    // Mảng chứa các hình ảnh Buff có thể được áp dụng
    public Sprite[] BuffsList;

    // Đối tượng Panel chứa thông tin Buff và AnimationPanel
    public GameObject AnimationPanel;
    public GameObject Panel;

    // Biến lưu trữ ID của Buff
    private int BuffID;

    // Biến lưu trữ ID của Buff trước đó
    private int PreBuffID;

    // Tham chiếu đến đối tượng Player
    private Player player;

    // Tham chiếu đến đối tượng WeaponHolder
    private WeaponHolder weaponHolder;

    // Biến kiểm tra trạng thái của Panel (mở hay đóng)
    public bool Open = true;

    // Start is called before the first frame update
    void Start()
    {
        // Lấy tham chiếu đến đối tượng Player và WeaponHolder
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();

        // Nếu có đối tượng Buff, tạo Buff ngẫu nhiên và hiển thị
        if (Buff != null)
        {
            BuffID = Random.Range(0, 12);
            PreBuffID = BuffID;
            Buff.sprite = BuffsList[BuffID];
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Cập nhật điểm kinh nghiệm hiện tại
        if (Exp != null) Exp.text = player.EXP.ToString();
    }

    // Phương thức xử lý khi nhấn nút Ascend
    public void Ascend()
    {
        // Kiểm tra xem người chơi có đủ điểm kinh nghiệm để Ascend không
        if (player.EXP >= 1)
        {
            // Áp dụng Buff, giảm điểm kinh nghiệm và tạo Buff mới
            ApplyBuff();
            player.EXP--;
            BuffID = RandomBuff();
            PreBuffID = BuffID;
            Buff.sprite = BuffsList[BuffID];
        }
    }

    // Phương thức xử lý khi nhấn nút Reroll Buff
    public void RerollBuff()
    {
        // Kiểm tra xem người chơi có đủ Coin để Reroll Buff không
        if (player.CurCoin >= 20)
        {
            // Reroll Buff mới và kiểm tra tránh trùng lặp với Buff trước đó
            BuffID = RandomBuff();
            while (BuffID == PreBuffID)
            {
                BuffID = RandomBuff();
            }
            // Lưu trữ Buff mới và giảm Coin
            PreBuffID = BuffID;
            Buff.sprite = BuffsList[BuffID];
            player.CurCoin -= 20;
        }
    }

    // Phương thức áp dụng Buff
    void ApplyBuff()
    {
        if (BuffID == 0 || BuffID == 4 || BuffID == 8)
        {
            // Tăng máu tối đa và thêm máu tương ứng
            player.PlayerMaxHP = player.PlayerMaxHP + 1 + BuffID / 4;
            player.AddHp(1 + BuffID / 4);
        }
        else if (BuffID == 1 || BuffID == 5 || BuffID == 9)
        {
            // Tăng mana tối đa và thêm mana tương ứng
            weaponHolder.MaxMana += 5 * (BuffID - 1) / 4;
            weaponHolder.AddMana(5 * (BuffID - 1) / 4);
        }
        else if (BuffID == 2 || BuffID == 6 || BuffID == 10)
        {
            // Tăng tỷ lệ Crit
            weaponHolder.CritRate = weaponHolder.CritRate + 2 * (BuffID - 2) / 4 + 1;
        }
        else if (BuffID == 3 || BuffID == 7 || BuffID == 11)
        {
            // Tăng sát thương Crit
            weaponHolder.CritDmg = weaponHolder.CritDmg + 2 * (BuffID - 2) / 4 + 1;
        }
    }

    // Phương thức xử lý khi nhấn nút Exit
    public void Exit()
    {
        // Hiển thị AnimationPanel và tắt Panel hiện tại
        AnimationPanel.SetActive(true);
        AnimationPanel.GetComponent<AscendController>().Open = false;
        AnimationPanel.GetComponent<Animator>().SetBool("Close", true);
        Panel.SetActive(false);
    }

    // Phương thức kích hoạt Panel
    public void Active()
    {
        // Nếu Panel đang mở, hiển thị Panel và tắt đối tượng hiện tại
        if (Open)
        {
            Panel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    // Phương thức tắt Panel
    public void DeActive()
    {
        // Nếu Panel đang tắt, kích hoạt lại Panel và tắt AnimationPanel
        if (!Open)
        {
            AnimationPanel.GetComponent<AscendController>().Open = true;
            AnimationPanel.SetActive(false);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<CallMenu>().BGPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Phương thức chọn Buff ngẫu nhiên
    int RandomBuff()
    {
        int Rand = Random.Range(1, 11);
        if (Rand >= 1 && Rand <= 6)
        {
            return Random.Range(0, 4);
        }
        else if (Rand >= 7 && Rand <= 9)
        {
            return Random.Range(4, 8);
        }
        else
        {
            return Random.Range(8, 12);
        }
    }
}
