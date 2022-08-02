using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float delayShoot;
    private float currentDelayShoot;
    private bool hasEnemyInRage;
    private Transform enemyTrans;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform muzzleTrans;
    [SerializeField] private Transform bodyTrans;
    private Queue<Enemy> enemiesInRange;
    void Start()
    {
        enemiesInRange = new Queue<Enemy>();
        currentDelayShoot = delayShoot;
    }

    void Update()
    {
        if (enemiesInRange.Count <= 0) return;

        Vector2 enemyDir = this.transform.position - enemiesInRange.Peek().transform.position;
        float rotZ = Mathf.Atan2(enemyDir.y, enemyDir.x) * Mathf.Rad2Deg;
        bodyTrans.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (currentDelayShoot < 0)
        {
            Shoot(enemyDir * -1);
            currentDelayShoot = delayShoot;
        }
        else
        {
            currentDelayShoot -= Time.deltaTime;
        }
    }

    private void Shoot(Vector2 direction)
    {
        Bullet b = Instantiate(bulletPrefab, muzzleTrans.position, Quaternion.identity);
        b.transform.SetParent(this.transform);
        b.ShootTo(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            enemiesInRange.Enqueue(e);
            hasEnemyInRage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Dequeue();
            SetEnemyTarget();
        }
    }

    private void SetEnemyTarget()
    {
        if (enemiesInRange.Count <= 0) return;
        this.enemyTrans = enemiesInRange.Peek().transform;
    }



}
