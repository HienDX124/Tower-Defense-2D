using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : BulletEffectBase
{
    private TurretBase turret => bulletComponent.turret;
    [SerializeField] private GameObject iceRegionPrefab;

    protected override void CauseEffect(Enemy enemyTarget)
    {
        base.CauseEffect(enemyTarget);
    }

    private void CreateIceRegion(Vector3 position)
    {

    }
}
