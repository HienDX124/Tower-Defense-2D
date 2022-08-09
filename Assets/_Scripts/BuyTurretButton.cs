using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyTurretButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text turretNameText;
    [SerializeField] private Text turretPriceText;
    private TurretInfo turretInfo;

    private void OnEnable()
    {
        button.onClick.AddListener(BuyButtonOnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(BuyButtonOnClick);
    }

    public void Init(TurretInfo turretInfo)
    {
        this.turretInfo = turretInfo;
        turretNameText.text = turretInfo.turretName;
        turretPriceText.text = turretInfo.price.ToString();
    }

    private void BuyButtonOnClick()
    {
        EventDispatcher.Instance.PostEvent(EventID.StartBuyTurret, turretInfo);
    }
}
