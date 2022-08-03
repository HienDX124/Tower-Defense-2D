using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyTurretButton : MonoBehaviour
{
    [SerializeField] private Button button;

    public TurretInfo turretInfo;

    private void OnEnable()
    {
        button.onClick.AddListener(BuyButtonOnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(BuyButtonOnClick);
    }

    private void BuyButtonOnClick()
    {
        EventDispatcher.Instance.PostEvent(EventID.BuyTurret, turretInfo);
    }
}
