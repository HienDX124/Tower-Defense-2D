using UnityEngine;

[System.Serializable]
public struct TurretInfo
{
    public string turretName;
    public TurretType turretType;
    public float activeRadius;
    public float delayShoot;
    public TurretTargetType targetType;
    public GameObject icon;
    public int price;
    public BulletInfo bulletInfo;
}

[System.Serializable]
public struct BulletInfo
{
    public TurretType turretType;
    public float damage;

}
