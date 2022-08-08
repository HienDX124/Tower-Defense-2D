using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : SingletonMonobehaviour<GameplayUI>
{
    [SerializeField] private Text coinsText;
    [SerializeField] private CanvasGroup turretShopCvg;
    [SerializeField] private Button turretShopBtn;
    [SerializeField] private Button closeTurretShopBtn;
    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.UpdateCoin, UpdateCoinUI);
        turretShopBtn.onClick.AddListener(ShowTurretShop);
        closeTurretShopBtn.onClick.AddListener(HideTurretShop);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.UpdateCoin, UpdateCoinUI);
        turretShopBtn.onClick.RemoveListener(ShowTurretShop);
        closeTurretShopBtn.onClick.RemoveListener(HideTurretShop);
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    private void UpdateCoinUI(object param = null)
    {
        coinsText.text = "Coins: " + GameManager.instance.levelCoins.ToString();
    }

    private void ShowTurretShop()
    {
        EnableTurretShop(true);
        // Play sound
    }

    private void HideTurretShop()
    {
        EnableTurretShop(false);
        // Play sound
    }

    private void EnableTurretShop(bool enable)
    {
        if (enable)
        {
            turretShopCvg.DOFade(1f, 0.3f).Play();
        }
        else
        {
            turretShopCvg.DOFade(0f, 0.3f).Play();
        }
        turretShopCvg.blocksRaycasts = enable;
    }

}