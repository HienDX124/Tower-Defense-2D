using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public abstract class BulletEffectBase : MonoBehaviour
{
    [SerializeField] protected float totalPoisonDur;
    protected Bullet bulletComponent;

    private void Awake()
    {
        bulletComponent = GetComponent<Bullet>();
    }

    protected abstract void OnCollisionEnter2D(Collision2D other);

    protected abstract void CauseEffect(Enemy enemyTarget);

}
