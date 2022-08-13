using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class PoisonEffect : BulletEffectBase
{
    [SerializeField] private float totalDamage;
    [SerializeField] private float timeStep;

    protected override void CauseEffect(Enemy enemyTarget)
    {
        base.CauseEffect(enemyTarget);
        StartCoroutine(CausePoisonEffect(enemyTarget));
    }

    private IEnumerator CausePoisonEffect(Enemy enemyTarget)
    {
        float damageRate = timeStep / totalEffectDur;
        do
        {
            yield return ExtensionClass.GetWaitForSeconds(timeStep);
            if (enemyTarget) enemyTarget.TakeDamage(totalDamage * damageRate);
            totalEffectDur -= timeStep;
        } while (totalEffectDur > 0);
    }
}