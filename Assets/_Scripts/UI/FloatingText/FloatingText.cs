using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    private bool _active;
    public bool active => _active;
    [SerializeField] private Text textComponent;
    [SerializeField] private CanvasGroup canvasGroup;
    private Sequence mainSequence;

    public void Show(string msg, int fontSize, Color color, Vector3 startPos, Vector3 endPos, float duration)
    {
        _active = true;
        canvasGroup.alpha = 1f;
        textComponent.text = msg;
        textComponent.fontSize = fontSize;
        textComponent.color = color;
        this.transform.position = startPos;

        mainSequence = MoveUpAndFadeOut(endPos, duration);

        mainSequence
            .OnComplete(() => _active = false)
            .OnKill(() => _active = false)
            .Play();
    }

    private Sequence MoveUpAndFadeOut(Vector3 endPos, float duration)
    {
        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(this.transform.DOMove(endPos, duration))
            .Join(this.canvasGroup.DOFade(0f, duration));
        return sequence;
    }
}