using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CommonFunctions : MonoBehaviour
{
    public static List<int> CountSubString(string subString, string dataString)
    {
        List<int> positions = new List<int>();
        int pos = 0;
        while ((pos < dataString.Length) && (pos = dataString.IndexOf(subString, pos)) != -1)
        {
            positions.Add(pos);
            pos += subString.Length;
        }

        return positions;
    }

    public static void PlayOneShotASound(AudioSource audioSource, AudioClip audioClip) => audioSource.PlayOneShot(audioClip);

    public static void EnableByCanvasGroup(CanvasGroup canvasGroup, bool enable, float delay = Const.PANEL_SLIDE_SPEED)
    {
        if (enable) canvasGroup.DOFade(1f, delay).Play();
        else canvasGroup.DOFade(0f, delay).Play();
        canvasGroup.blocksRaycasts = enable;
    }

    public static bool MouseOnElement(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        float xMin = corners[0].x;
        float xMax = corners[2].x;

        float yMin = corners[0].y;
        float yMax = corners[2].y;

        if ((Input.mousePosition.x > xMax || Input.mousePosition.x < xMin)
            ||
            (Input.mousePosition.y > yMax || Input.mousePosition.y < yMin))
        {
            return false;
        }

        return true;
    }

}
