using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : SingletonMonobehaviour<BulletManager>
{
    [SerializeField] private BulletDataSO bulletDataSO;
    public List<BulletInfo> bulletInfoList => bulletDataSO.bulletInfos;
    [SerializeField] private BulletInfo bulletInfoDefault;

    public BulletInfo GetBulletInfo(TurretType turretType)
    {
        foreach (BulletInfo bulletInfo in bulletInfoList)
        {
            if (bulletInfo.turretType == turretType) return bulletInfo;
        }
        return bulletInfoDefault;
    }
}

