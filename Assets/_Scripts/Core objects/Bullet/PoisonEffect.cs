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
        StartCoroutine(CausePoisonEffect(enemyTarget));
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {

    }

    private IEnumerator CausePoisonEffect(Enemy enemyTarget)
    {
        Debug.LogWarning("CauseDamagePoison");
        do
        {
            Debug.LogWarning("Damage poison");
            yield return ExtensionClass.GetWaitForSeconds(timeStep);
            if (enemyTarget) enemyTarget.TakeDamage(totalDamage * (timeStep / totalPoisonDur));
            totalPoisonDur -= timeStep;
        } while (totalPoisonDur > 0);
    }
}
