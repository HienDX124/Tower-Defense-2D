using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet data", menuName = "Scriptable objects/BulletData")]
public class BulletDataSO : ScriptableObject
{
    [SerializeField] private BulletInfo[] bulletInfos;
    public BulletInfo[] BulletInfos;
}
