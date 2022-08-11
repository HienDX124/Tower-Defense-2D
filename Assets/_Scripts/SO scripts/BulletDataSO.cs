using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet data", menuName = "Scriptable objects/BulletData")]
public class BulletDataSO : ScriptableObject
{
    public List<BulletInfo> bulletInfos;
}
