using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;
    public CanvasGroup canvasGroup;
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
        canvasGroup.alpha = 1f;
        _ = FadeOutAfter((int)(duration * 500));
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    private async UniTask FadeOutAfter(int delay)
    {
        await UniTask.Delay(delay);
        canvasGroup.DOFade(0f, 0.5f)
            .Play();
    }

    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }
        if (Time.time - lastShown > duration)
        {
            Hide();
        }

        go.transform.position += motion * Time.deltaTime;

    }

}