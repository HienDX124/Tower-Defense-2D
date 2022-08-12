using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret data", menuName = "Scriptable objects/TurretData")]
public class TurretDataSO : ScriptableObject
{
    [SerializeField] protected TurretInfo[] turretInfos;
    public TurretInfo[] TurretInfos => turretInfos;

    private void OnValidate()
    {
        for (int i = 0; i < turretInfos.Length; i++)
        {
            turretInfos[i].turretName = turretInfos[i].turretType.ToString();
        }
    }



}
