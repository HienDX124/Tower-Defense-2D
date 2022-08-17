using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    private PathCreator mainPath;
    [SerializeField] private Transform levelRootTrans;
    [SerializeField] private LevelInfo[] levelInfoPrefabs;
    private int _levelCoins;
    public int levelCoins { get => _levelCoins; }
    public List<Vector3> mainPathPoints => mainPath.getPoints();
    private int _temporaryTotalEnemies;
    protected override void Awake()
    {
        base.Awake();
        co = StartPlay();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.EnemyDie, HandleEventEnemyDie);
        EventDispatcher.Instance.RegisterListener(EventID.EnemyReachBase, HandleEventEnemyReachBase);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.EnemyDie, HandleEventEnemyDie);
        EventDispatcher.Instance.RemoveListener(EventID.EnemyReachBase, HandleEventEnemyReachBase);
    }

    private void HandleEventEnemyDie(object param = null)
    {
        Enemy e = (Enemy)param;
        IncreaseCoins(e.coinReward);
        _temporaryTotalEnemies--;
        if (_temporaryTotalEnemies == 0) WinLevel();
    }

    private void HandleEventEnemyReachBase(object param = null)
    {
        Enemy e = (Enemy)param;
        LoseLevel();
    }

    public void IncreaseCoins(int amount)
    {
        _levelCoins += amount;
        EventDispatcher.Instance.PostEvent(EventID.UpdateCoin);
    }

    LevelInfo currentLevelInfo;

    public void LoadLevel(bool isWin = false)
    {
        WinLosePanel.instance.ShowPanel(false, false);

        if (levelRootTrans.childCount > 0) Destroy(levelRootTrans.GetChild(0).gameObject);

        if (UserData.LevelNumber >= levelInfoPrefabs.Length)
            currentLevelInfo = Instantiate<LevelInfo>(levelInfoPrefabs.PickRandom(), levelRootTrans);
        else
            currentLevelInfo = Instantiate<LevelInfo>(levelInfoPrefabs[UserData.LevelNumber], levelRootTrans);

        mainPath = currentLevelInfo.mainPath;
        int totalEnemy = 0;
        foreach (WaveInfo waveInfo in currentLevelInfo.waveInfos)
        {
            totalEnemy += waveInfo.numOfEnemyInWave;
        }
        _temporaryTotalEnemies = totalEnemy;

        _levelCoins = 0;
        IncreaseCoins(currentLevelInfo.startCoins);
        EnemyManager.instance.Init(totalEnemy);
        EventDispatcher.Instance.PostEvent(EventID.LoadLevel);
        co = StartPlay();
    }

    public void WinLevel()
    {
        DOTween.KillAll();
        UserData.LevelNumber++;
        WinLosePanel.instance.ShowPanel(true, true);
        EventDispatcher.Instance.PostEvent(EventID.EndLevel, true);
        StopCoroutine(co);
    }

    public void LoseLevel()
    {
        DOTween.KillAll();
        WinLosePanel.instance.ShowPanel(true, false);
        EventDispatcher.Instance.PostEvent(EventID.EndLevel, false);
        StopCoroutine(co);
    }

    public IEnumerator co;
    private IEnumerator StartPlay()
    {
        foreach (WaveInfo waveInfo in currentLevelInfo.waveInfos)
        {
            EnemyManager.instance.EnemiesStartWave(waveInfo.numOfEnemyInWave, waveInfo.delayPerEnemy);
            yield return ExtensionClass.GetWaitForSeconds((float)(waveInfo.delayPerEnemy) / 1000);
        }
    }
}

