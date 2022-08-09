using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : SingletonMonobehaviour<BulletManager>
{
    [SerializeField] private BulletDataSO bulletDataSO;
    public List<BulletInfo> bulletInfoList => bulletDataSO.bulletInfos;

    public BulletInfo GetBulletInfo(TurretType turretType)
    {
        foreach (BulletInfo bulletInfo in bulletInfoList)
        {
            if (bulletInfo.turretType == turretType) return bulletInfo;
        }
        return bulletInfoList[0];
    }

}

[CreateAssetMenu(fileName = "Bullet data", menuName = "Scriptable objects/BulletData")]
public class BulletDataSO : ScriptableObject
{
    public List<BulletInfo> bulletInfos;
}
