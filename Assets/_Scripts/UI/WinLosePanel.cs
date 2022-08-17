using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLosePanel : SingletonMonobehaviour<WinLosePanel>
{
    [SerializeField] private Text anounceText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private string winMsg;
    [SerializeField] private string loseMsg;
    [SerializeField] private CanvasGroup canvasGroup;
    private void OnEnable()
    {
        replayButton.onClick.AddListener(ReplayButtonOnClick);
        nextLevelButton.onClick.AddListener(NextLevelButtonOnClick);
    }

    private void OnDisable()
    {
        replayButton.onClick.RemoveListener(ReplayButtonOnClick);
        nextLevelButton.onClick.RemoveListener(NextLevelButtonOnClick);
    }

    private void Start()
    {
        CommonFunctions.EnableByCanvasGroup(canvasGroup, false, 0);
    }

    private void ReplayButtonOnClick()
    {
        GameManager.instance.LoadLevel(false);
    }

    private void NextLevelButtonOnClick()
    {
        GameManager.instance.LoadLevel(true);
    }

    public void ShowPanel(bool enable, bool isWin)
    {
        CommonFunctions.EnableByCanvasGroup(canvasGroup, enable);
        replayButton.gameObject.SetActive(!isWin);
        nextLevelButton.gameObject.SetActive(isWin);
        string textToShow = (isWin) ? winMsg : loseMsg;
        anounceText.text = textToShow;
    }
}
