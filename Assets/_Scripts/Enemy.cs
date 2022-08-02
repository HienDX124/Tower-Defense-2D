using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Vector3> movePath;
    private Tween movingTween;
    private float currentHP;
    private float hpOrigin;
    public int enemyID;
    [SerializeField] private HPBar hPBar;
    public int coinReward;
    public void Init(float hp, int ID, List<Vector3> movePath)
    {
        this.hpOrigin = hp;
        this.currentHP = hpOrigin;
        this.enemyID = ID;
        this.movePath = movePath;
    }

    public void StartMove()
    {
        List<Vector3> pathPointList = movePath;
        movingTween = this.transform.DOPath(pathPointList.ToArray(), 10f);
        movingTween.SetEase(Ease.Linear);

        this.transform.position = movePath[0];
        movingTween.Play();
    }

    public void TakeDamage(float amount)
    {
        UpdateHP(-amount);
        hPBar.UpdateHP(currentHP, hpOrigin);
        if (currentHP <= 0)
            Death();
    }

    private void UpdateHP(float amount)
    {
        this.currentHP += amount;
    }

    private void RecoverHP(float amount)
    {
        UpdateHP(amount);
    }

    public void Death()
    {
        movingTween.Kill();
        Destroy(this.gameObject);
        EventDispatcher.Instance.PostEvent(EventID.EnemyDie, this);
    }

}
