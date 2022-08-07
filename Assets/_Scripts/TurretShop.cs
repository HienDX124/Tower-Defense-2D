using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TurretShop : SingletonMonobehaviour<TurretShop>
{
    public List<TurretInfo> turretDataList;
    private List<BuyTurretButton> buttonList;
    [SerializeField] private BuyTurretButton buyTurretButtonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private CanvasGroup turretInstanceCvg;
    [SerializeField] private Turret turretInstance;
    [SerializeField] private Turret turretPrefab;
    private TurretInfo turretInstanceInfo;
    private CanvasGroup turretShopCvg;
    [SerializeField] private Button buildButton;
    [SerializeField] private Button cancelButton;
    private bool isBuyingTurret;
    private Vector2 turretInstancePos;
    protected override void Awake()
    {
        base.Awake();

        buttonList = new List<BuyTurretButton>();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.StartBuyTurret, SetupTurretInstance);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.StartBuyTurret, SetupTurretInstance);
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (var ti in turretDataList)
        {
            BuyTurretButton button = Instantiate<BuyTurretButton>(buyTurretButtonPrefab, buttonContainer);
            buttonList.Add(button);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateTurretInstanceUIPos(Input.mousePosition);
        }
    }

    private void SetupTurretInstance(object param = null)
    {
        turretInstanceInfo = (TurretInfo)param;
        turretInstance.Init(turretInstanceInfo);
        CommonFunctions.EnableByCanvasGroup(turretInstanceCvg, true);
    }

    public void UpdateTurretInstanceUIPos(Vector3 mousePos)
    {
        turretInstanceCvg.transform.position = (Vector2)mousePos;
        turretInstance.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
        turretInstance.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
        turretInstancePos = turretInstance.transform.position;
    }

    private void BuildButtonOnClick()
    {
        int turretPrice = turretInstanceInfo.price;
        if (UserData.CoinsNumber < turretPrice) return;

        CommonFunctions.EnableByCanvasGroup(turretInstanceCvg, false, 0f);

        GameManager.instance.IncreaseCoins(turretPrice);

        Turret newTurret = Instantiate<Turret>(turretPrefab, turretInstancePos, Quaternion.identity);
    }

}
