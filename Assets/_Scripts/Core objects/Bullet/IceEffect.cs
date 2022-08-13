using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : BulletEffectBase
{
    private TurretBase turret => bulletComponent.turret;
    [SerializeField] private IceRegion iceRegionPrefab;
    [SerializeField] private float slowRate;
    [SerializeField] private float maxRangeRadius;
    [SerializeField] private float showDur;
    [SerializeField] private float disappearDur;
    protected override void CauseEffect(Enemy enemyTarget)
    {
        base.CauseEffect(enemyTarget);
        CreateIceRegion(collidePos);
    }

    private void CreateIceRegion(Vector3 position)
    {
        IceRegion iceRegion = Instantiate<IceRegion>(iceRegionPrefab, turret.effectContainerTrans);
        iceRegion.Init(position);
        iceRegion.Active(totalEffectDur, slowRate, maxRangeRadius, showDur, disappearDur);
    }
}
