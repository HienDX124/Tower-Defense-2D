using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float maxRange;
    [SerializeField] private float forceIntensity;
    private Tween movingTween;
    [SerializeField] private BulletInfo bulletInfo;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private TurretBase _turret;
    public TurretBase turret => _turret;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(BulletInfo info)
    {
        this.bulletInfo = info;
    }

    public void ShootTo(Vector2 dir, TurretBase turret)
    {
        rigid.AddForce(forceIntensity * (dir));
        Destroy(this.gameObject, 10f);
        this._turret = turret;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.TakeDamage(bulletInfo.damage);
            Explode();
        }
    }

    private void Explode()
    {
        //  Play FX explode
        _collider2D.enabled = false;
        spriteRenderer.enabled = false;
    }
}
