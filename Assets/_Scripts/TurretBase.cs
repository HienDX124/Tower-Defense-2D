using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [SerializeField] private float delayShoot;
    private float currentDelayShoot;
    [SerializeField] private Transform muzzleTrans;
    [SerializeField] private Transform bodyTrans;
    [SerializeField] private float activeRadius;
    [SerializeField] private CircleCollider2D _circleCollider2D;
    private Queue<Enemy> enemiesInRange;
    [SerializeField] private Transform shootRangeImgTrans;
    private bool _isActive;
    [SerializeField] private SpriteRenderer bodyIcon;
    [SerializeField] private SpriteRenderer barrelIcon;
    private TurretType type;
    [SerializeField] private Transform effectContainerTrans;

    private void Awake()
    {
        enemiesInRange = new Queue<Enemy>();
    }

    private void Start()
    {
        currentDelayShoot = delayShoot;
    }

    private void OnValidate()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = activeRadius;
        shootRangeImgTrans.localScale = Vector3.one * (activeRadius * 2);
    }

    private void Update()
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
        BulletInfo bulletToShoot = BulletManager.instance.GetBulletInfo(type);
        Bullet b = Instantiate(bulletToShoot.bulletPrefab, muzzleTrans.position, Quaternion.identity);
        b.Init(bulletToShoot);
        b.transform.SetParent(this.transform);
        b.ShootTo(direction, this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            enemiesInRange.Enqueue(e);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Dequeue();
        }
    }

    public void Init(TurretInfo turretInfo)
    {
        this._circleCollider2D.radius = turretInfo.activeRadius;
        this.shootRangeImgTrans.localScale = Vector3.one * turretInfo.activeRadius * 2;
        this.delayShoot = turretInfo.delayShoot;
        this.bodyIcon.sprite = turretInfo.bodyIcon;
        this.barrelIcon.sprite = turretInfo.barrelIcon;
        this.type = turretInfo.turretType;
    }

    public void EnableTurret(bool enable)
    {
        _circleCollider2D.enabled = enable;
        gameObject.SetActive(enable);
        _isActive = (enable);
    }
}
