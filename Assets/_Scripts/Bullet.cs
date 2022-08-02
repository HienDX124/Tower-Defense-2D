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
    [SerializeField] private float damageCause;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void ShootTo(Vector2 dir)
    {
        rigid.AddForce(forceIntensity * (dir));
        Destroy(this.gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.TakeDamage(damageCause);
            Explode();
        }
    }

    public void Explode()
    {
        Destroy(this.gameObject);
    }
}
