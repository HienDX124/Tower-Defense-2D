using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : SingletonMonobehaviour<TurretManager>
{
    [SerializeField] private TurretDataSO turretDataSO;
    public TurretInfo[] turretInfos => turretDataSO.TurretInfos;
    private List<TurretBase> turretInLevelList;

    protected override void Awake()
    {
        base.Awake();
        turretInLevelList = new List<TurretBase>();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.LoadLevel, HandleEventLoadLevel);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.LoadLevel, HandleEventLoadLevel);
    }

    private void HandleEventLoadLevel(object param = null)
    {
        ClearAllTurret();
    }

    public void AddNewTurretBuilt(TurretBase turret)
    {
        turretInLevelList.Add(turret);
    }

    private void ClearAllTurret()
    {
        foreach (TurretBase turret in turretInLevelList)
        {
            Destroy(turret.gameObject);
        }
        turretInLevelList.Clear();
    }

}
