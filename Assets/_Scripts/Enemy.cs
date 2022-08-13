using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    private bool onFireEffect;
    [SerializeField] private SpriteRenderer enemyIcon;

    public void Init(float hp, int ID, List<Vector3> movePath)
    {
        this.hpOrigin = hp;
        this.currentHP = hpOrigin;
        this.enemyID = ID;
        this.movePath = movePath;
    }

    public async UniTask StartMove(int delay)
    {
        List<Vector3> pathPointList = movePath;
        movingTween = this.transform.DOPath(pathPointList.ToArray(), 10f);
        movingTween.SetEase(Ease.Linear);
        this.transform.position = movePath[0];

        await UniTask.Delay(delay);
        movingTween.Play();
    }

    public void TakeDamage(float amount)
    {
        if (onFireEffect) amount *= 2;
        UpdateHP(-amount);
        FloatingTextManager.instance.ShowUpdateHP((-amount).ToString(), this.transform.position);
        hPBar.UpdateHP(currentHP, hpOrigin);
        if (currentHP <= 0)
            Death();
    }

    private void UpdateHP(float amount) => this.currentHP += amount;

    private void RecoverHP(float amount) => UpdateHP(amount);

    public void Death()
    {
        movingTween.Kill();
        Destroy(this.gameObject);
        EventDispatcher.Instance.PostEvent(EventID.EnemyDie, this);
    }

    public async UniTask GetFireEffect(float effectDur)
    {
        onFireEffect = true;
        await UniTask.Delay((int)(effectDur * 1000));
        onFireEffect = false;
    }

    public void ShowEffect(float duration, TurretType effectType)
    {
        Color color = BulletManager.instance.GetEffectColor(effectType);
        this.enemyIcon.DOColor(color, duration / 5)
            .SetLoops(5, LoopType.Yoyo)
            .OnComplete(() => enemyIcon.color = Color.white)
            .Play();
    }
}