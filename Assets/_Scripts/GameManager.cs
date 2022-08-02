using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{

    [SerializeField] private PathCreator mainPath;

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

    private void Start()
    {
        EnemyManager.instance.Init();
    }

    private void HandleEnemyDie(object param = null)
    {
        Enemy e = (Enemy)param;
        IncreaseCoins(e.coinReward);
    }

    private void IncreaseCoins(int amount)
    {
        UserData.CoinsNumber += amount;
        EventDispatcher.Instance.PostEvent(EventID.UpdateCoin);
    }

}

