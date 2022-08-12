using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : BulletEffectBase
{
    protected override void CauseEffect(Enemy enemyTarget)
    {
        CauseFireEffect(enemyTarget);
    }

    private void CauseFireEffect(Enemy enemyTarget)
    {
        enemyTarget.GetFireEffect();
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {

    }

}
