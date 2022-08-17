using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IceRegion : MonoBehaviour
{
    private Sequence activeSequence;
    [SerializeField] private SpriteRenderer icon;
    private float slowRate;
    private float slowDur;

    private void Awake()
    {
        activeSequence = DOTween.Sequence();
    }

    public void Init(Vector3 position)
    {
        this.transform.position = position;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _ = other.gameObject.GetComponent<Enemy>().GetIceEffect(this.slowRate, slowDur);
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _ = other.gameObject.GetComponent<Enemy>().GetIceEffect(this.slowRate, slowDur);
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Active(float slowDur, float slowRate, float maxRangeRadius, float showDur, float disappearDur)
    {
        this.slowRate = slowRate;
        this.slowDur = slowDur;
        this.transform.localScale = Vector3.zero;

        activeSequence.Append(this.transform.DOScale(Vector3.one * maxRangeRadius, showDur));
        activeSequence.Append(this.icon.DOFade(0f, disappearDur));

        activeSequence
            .OnComplete(() => Destroy(this.gameObject, 0.1f))
            .Play()
            ;
    }

}
