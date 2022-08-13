using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public abstract class BulletEffectBase : MonoBehaviour
{
    [SerializeField] protected float totalEffectDur;
    protected Bullet bulletComponent;
    [SerializeField] protected TurretType effectType;

    private void Awake()
    {
        bulletComponent = GetComponent<Bullet>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            CauseEffect(enemy);
        }
    }

    protected virtual void CauseEffect(Enemy enemyTarget)
    {
        enemyTarget.ShowEffect(totalEffectDur, effectType);
    }
}