using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMenu : MonoBehaviour
{
    // Biến lưu trữ thời gian chơi tổng cộng
    public float TotalTime;

    // Các đối tượng giao diện
    public GameObject BGPanel;
    public GameObject ShopMenu;
    public GameObject AscensionMenu;
    public GameObject ChangeScene;
    public GameObject Ending;
    public GameObject EndingPanel;
    public GameObject ResultMenu;
    public GameObject PauseMenu;
    public GameObject GameOverPanel;
    public GameObject[] ObjectsDestroyToReset;
    public GameObject ChangeScencePanel;
    public GameObject MiniCam;

    void Update()
    {
        // Cập nhật thời gian chơi tổng cộng
        TotalTime += Time.deltaTime;

        // Nếu nhấn phím P và PauseMenu không hoạt động, tạm dừng trò chơi
        if (Input.GetKeyDown(KeyCode.P) && !PauseMenu.activeSelf)
        {
            PauseGame();
        }
    }

    // Phương thức hủy các đối tượng để chuẩn bị cho việc reset
    public void DestroyToReset()
    {
        for (int i = 1; i < 4; i++)
        {
            Destroy(ObjectsDestroyToReset[i]);
        }
    }

    // Hiển thị hoặc ẩn ShopMenu
    public void DisplayShopMenu(bool display)
    {
        BGPanel.SetActive(display);
        ShopMenu.SetActive(display);

        // Nếu đang hiển thị ShopMenu, tạm dừng thời gian
        if (display)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Thiết lập thông tin cho ShopMenu
    public void SetItemForShop(int ID, int i, int price, GameObject SR, bool IsPoison)
    {
        ShopMenuController shop = ShopMenu.GetComponent<ShopMenuController>();

        shop.ItemsID[i] = ID;
        shop.ItemIcons[i].sprite = SR.GetComponent<SpriteRenderer>().sprite;
        shop.ItemIcons[i].SetNativeSize();
        shop.Price[i].text = price.ToString();
        shop.IsPoison = IsPoison;

        // Hiển thị giao diện của cửa hàng Weapons hoặc Poison tùy thuộc vào loại NPC
        if (IsPoison)
        {
            shop.WeaponShop.SetActive(false);
            shop.PoisonShop.SetActive(true);
        }
        else
        {
            shop.WeaponShop.SetActive(true);
            shop.PoisonShop.SetActive(false);
        }
    }

    // Hiển thị AscensionMenu
    public void DisplayAscentionMenu()
    {
        Time.timeScale = 0;
        BGPanel.SetActive(true);
        AscensionMenu.SetActive(true);
    }

    // Hiển thị giao diện chuyển cảnh
    public void DisplayChange()
    {
        ChangeScene.SetActive(true);
        Time.timeScale = 0;
    }

    // Hiển thị giao diện kết thúc game
    public void DisplayEnding()
    {
        Ending.SetActive(true);
        EndingPanel.SetActive(true);
    }

    // Ẩn giao diện kết thúc game
    public void UnDisplayEnding()
    {
        Ending.SetActive(false);
    }

    // Hiển thị giao diện kết quả
    public void DisplayResult()
    {
        ResultMenu.SetActive(true);
        ResultMenuController resultMenuController = ResultMenu.GetComponent<ResultMenuController>();
        resultMenuController.time.text = UpdateLevelTimer(TotalTime);
    }

    // Cập nhật thời gian chơi thành chuỗi định dạng phút:giây
    public string UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }
        string Str = minutes.ToString("00") + ":" + seconds.ToString("00");

        return Str;
    }

    // Tạm dừng trò chơi và hiển thị PauseMenu
    public void PauseGame()
    {
        Time.timeScale = 0;
        BGPanel.SetActive(true);
        PauseMenu.SetActive(true);
        PausedMenuControll pausedMenuControll = PauseMenu.GetComponent<PausedMenuControll>();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
        WeaponHolder weaponHolder = GameObject.FindGameObjectWithTag("WeaponPos").GetComponent<WeaponHolder>();

        // Hiển thị thông tin về máu, mana, tỷ lệ crit và sát thương crit
        pausedMenuControll.MaxHp.text = player.PlayerMaxHP.ToString();
        pausedMenuControll.MaxMp.text = weaponHolder.MaxMana.ToString();
        pausedMenuControll.CritRate.text = weaponHolder.CritRate + "%";
        pausedMenuControll.CritDmg.text = weaponHolder.CritDmg + "%";
    }

    // Hiển thị giao diện Game Over
    public void GameOver()
    {
        Time.timeScale = 0;
        MiniCam.SetActive(false);
        GameOverPanel.SetActive(true);
        BGPanel.SetActive(true);

        GameOverController gameOverController = GameOverPanel.GetComponent<GameOverController>();
        gameOverController.DeadAnimaton.GetComponent<RectTransform>().position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Hiệu ứng chuyển cảnh khi vào cổng
    public void ChangeSceneEffect()
    {
        ChangeScencePanel.SetActive(true);
        MiniCam.SetActive(false);
        Invoke(nameof(EndChangeScene), 1.5f);
        GetComponent<AudioSource>().Play();
    }

    // Kết thúc hiệu ứng chuyển cảnh
    void EndChangeScene()
    {
        ChangeScencePanel.SetActive(false);
        MiniCam.SetActive(true);
    }
}
