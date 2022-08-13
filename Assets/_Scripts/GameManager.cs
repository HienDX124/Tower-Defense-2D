using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{

    private PathCreator mainPath;
    [SerializeField] private Transform levelRootTrans;
    [SerializeField] private LevelInfo[] levelInfoPrefabs;
    private int _levelCoins;
    public int levelCoins { get => _levelCoins; }
    public List<Vector3> mainPathPoints => mainPath.getPoints();
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.EnemyDie, HandleEnemyDie);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.EnemyDie, HandleEnemyDie);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) _ = StartPlay();
    }

    private void HandleEnemyDie(object param = null)
    {
        Enemy e = (Enemy)param;
        IncreaseCoins(e.coinReward);
    }

    public void IncreaseCoins(int amount)
    {
        _levelCoins += amount;
        EventDispatcher.Instance.PostEvent(EventID.UpdateCoin);
    }

    LevelInfo currentLevelInfo;

    public void LoadLevel()
    {
        if (levelRootTrans.childCount > 0) Destroy(levelRootTrans.GetChild(0));

        currentLevelInfo = Instantiate<LevelInfo>(levelInfoPrefabs.PickRandom(), levelRootTrans);

        mainPath = currentLevelInfo.mainPath;
        int totalEnemy = 0;
        foreach (WaveInfo waveInfo in currentLevelInfo.waveInfos)
        {
            totalEnemy += waveInfo.numOfEnemyInWave;
        }
        _levelCoins = currentLevelInfo.startCoins;
        EnemyManager.instance.Init(totalEnemy);
        EventDispatcher.Instance.PostEvent(EventID.LoadLevel);
    }

    public async UniTask StartPlay()
    {
        foreach (WaveInfo waveInfo in currentLevelInfo.waveInfos)
        {
            EnemyManager.instance.EnemiesStartWave(waveInfo.numOfEnemyInWave, waveInfo.delayPerEnemy);
            await UniTask.Delay(currentLevelInfo.delayPerWave);
        }
    }
}

