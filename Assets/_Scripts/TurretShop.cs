using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
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
    [SerializeField] private Transform turretManagerTrans;
    protected override void Awake()
    {
        base.Awake();

        buttonList = new List<BuyTurretButton>();
        turretShopCvg = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.StartBuyTurret, SetupTurretInstance);
        buildButton.onClick.AddListener(BuildButtonOnClick);
        cancelButton.onClick.AddListener(CancelButtonOnClick);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.StartBuyTurret, SetupTurretInstance);
        buildButton.onClick.RemoveListener(BuildButtonOnClick);
        cancelButton.onClick.RemoveListener(CancelButtonOnClick);
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
            button.Init(ti);
            buttonList.Add(button);
        }
    }

    private void Update()
    {
        if (
            !isBuyingTurret
            || CommonFunctions.MouseOnElement(buildButton.GetComponent<RectTransform>())
            || CommonFunctions.MouseOnElement(cancelButton.GetComponent<RectTransform>())
            ) return;

        if (Input.GetMouseButtonDown(0))
        {
            UpdateTurretInstanceUIPos(Input.mousePosition);
        }
    }

    private void EnableTurretInstance(bool enable)
    {
        turretInstance.gameObject.SetActive(enable);
        CommonFunctions.EnableByCanvasGroup(turretInstanceCvg, enable);
    }

    private void SetupTurretInstance(object param = null)
    {
        isBuyingTurret = true;
        EnableTurretInstance(true);
        turretInstanceInfo = (TurretInfo)param;

        turretInstance.Init(turretInstanceInfo);
        CommonFunctions.EnableByCanvasGroup(turretInstanceCvg, true);
        CommonFunctions.EnableByCanvasGroup(turretShopCvg, false);
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
        if (GameManager.instance.levelCoins < turretPrice) return;

        GameManager.instance.IncreaseCoins(turretPrice * -1);

        Turret newTurret = Instantiate<Turret>(turretPrefab, turretInstancePos, Quaternion.identity, turretManagerTrans);
        newTurret.Init(turretInstanceInfo);

        isBuyingTurret = false;

        EnableTurretInstance(false);
    }

    private void CancelButtonOnClick()
    {
        isBuyingTurret = false;

        EnableTurretInstance(false);
        CommonFunctions.EnableByCanvasGroup(turretShopCvg, true);
    }
}
