using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : SingletonMonobehaviour<TurretManager>
{
    [SerializeField] private TurretDataSO turretDataSO;
    public TurretInfo[] turretInfos => turretDataSO.TurretInfos;



}
