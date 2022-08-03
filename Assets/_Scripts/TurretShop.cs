using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TurretShop : SingletonMonobehaviour<TurretShop>
{
    public List<TurretInfo> turretDataList;
    private List<BuyTurretButton> buttonList;
    [SerializeField] private BuyTurretButton buyTurretButtonPrefab;
    [SerializeField] private Transform buttonContainer;
    private CanvasGroup turretShopCvg;

    protected override void Awake()
    {
        base.Awake();

        buttonList = new List<BuyTurretButton>();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        Init();
    }

    public void EnableTurretShop(bool enable)
    {
        turretShopCvg.blocksRaycasts = enable;
        if (enable)
        {
            turretShopCvg.DOFade(1f, 0.3f);
        }
        else
        {
            turretShopCvg.DOFade(0f, 0.3f);
        }
    }

    public void Init()
    {
        foreach (var ti in turretDataList)
        {
            BuyTurretButton button = Instantiate<BuyTurretButton>(buyTurretButtonPrefab, buttonContainer);
            buttonList.Add(button);
        }
    }

}
