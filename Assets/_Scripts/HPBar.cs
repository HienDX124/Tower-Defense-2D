using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Transform pivotTrans;
    public void UpdateHP(float currentHp, float hpOrigin)
    {
        float remainingHpRate = currentHp / hpOrigin;
        if (remainingHpRate > 1f) remainingHpRate = 1f;
        if (remainingHpRate < 0f) remainingHpRate = 0f;

        pivotTrans.localScale = new Vector3(remainingHpRate, pivotTrans.localScale.y, pivotTrans.localScale.z);
    }

}
