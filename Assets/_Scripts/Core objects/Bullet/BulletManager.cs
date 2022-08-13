using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : SingletonMonobehaviour<BulletManager>
{
    [SerializeField] private BulletDataSO bulletDataSO;
    public BulletInfo[] bulletInfos => bulletDataSO.BulletInfos;
    [SerializeField] private BulletInfo bulletInfoDefault;

    public BulletInfo GetBulletInfo(TurretType turretType)
    {
        foreach (BulletInfo bulletInfo in bulletInfos)
        {
            if (bulletInfo.turretType == turretType) return bulletInfo;
        }
        return bulletInfoDefault;
    }

    public Color GetEffectColor(TurretType type)
    {
        foreach (BulletInfo bulletInfo in bulletInfos)
        {
            if (bulletInfo.turretType == type) return bulletInfo.effectColor;
        }
        return bulletInfoDefault.effectColor;
    }
}

