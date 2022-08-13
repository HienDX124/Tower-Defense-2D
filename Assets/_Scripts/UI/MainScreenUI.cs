using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenUI : SingletonMonobehaviour<MainScreenUI>
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button clearDataButton;
    [SerializeField] private Button selectLevelButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private CanvasGroup selectLevelPanelCanvasGroup;
    private CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayButtonOnClick);
        clearDataButton.onClick.AddListener(ClearDataButtonOnClick);
        selectLevelButton.onClick.AddListener(SelectLevelButtonOnClick);
        quitButton.onClick.AddListener(QuitButtonOnClick);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayButtonOnClick);
        clearDataButton.onClick.RemoveListener(ClearDataButtonOnClick);
        selectLevelButton.onClick.RemoveListener(SelectLevelButtonOnClick);
        quitButton.onClick.RemoveListener(QuitButtonOnClick);
    }

    public void EnablePanel(bool enable)
    {
        CommonFunctions.EnableByCanvasGroup(this.canvasGroup, enable);
    }

    private void PlayButtonOnClick()
    {
        GameManager.instance.LoadLevel();
        EnablePanel(false);
    }

    private void ClearDataButtonOnClick()
    {
        PlayerPrefs.DeleteAll();
    }

    private void SelectLevelButtonOnClick()
    {
        CommonFunctions.EnableByCanvasGroup(selectLevelPanelCanvasGroup, true);
    }

    private void QuitButtonOnClick()
    {
        Application.Quit();
    }

}
