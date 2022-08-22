using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : SingletonMonobehaviour<FloatingTextManager>
{
    public FloatingText textPrefab;
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private FloatingText GetFloatingText()
    {
        FloatingText floatingText = floatingTexts.Find(t => !t.active);
        if (floatingText == null)
        {
            floatingText = Instantiate(textPrefab, this.transform);
            floatingTexts.Add(floatingText);
        }
        return floatingText;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowMoveYRay("HienDX say hi", 35, Color.cyan, this.transform.position, 80f, 2f);
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 startPos, Vector3 endPos, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.Show(msg, fontSize, color, startPos, endPos, duration);
    }


    public void ShowMoveYRay(string msg, int fontSize, Color color, Vector3 startPos, float yRayChangeAmount, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        Vector3 endPos = new Vector3(startPos.x, startPos.y + yRayChangeAmount, startPos.z);
        floatingText.Show(msg, fontSize, color, startPos, endPos, duration);
    }

    public void ShowUpdateHP(string hp, Vector3 startPos)
    {

        ShowMoveYRay(hp, 35, Color.red, startPos, startPos.y + 5, 1.5f);
    }
}