using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyManager : SingletonMonobehaviour<EnemyManager>
{
    [SerializeField] private Enemy[] enemyPrefabs;
    public List<Enemy> enemyList;
    public int numOfEnemy;
    [SerializeField] private int delayPerEnemy;
    [SerializeField] private float enemyHP;
    private List<Vector3> enemyMovePath;
    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.EnemyDie, UpdateEnemyList);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.EnemyDie, UpdateEnemyList);
    }
    public void Init()
    {
        enemyMovePath = GameManager.instance.mainPathPoints;
        enemyList = new List<Enemy>();
        _ = SpawnEnemies(numOfEnemy, delayPerEnemy);
    }

    private Enemy SpawnEnemy(int id, Enemy enemyPrefab = null)
    {
        if (!enemyPrefab) enemyPrefab = enemyPrefabs.PickRandom();
        Enemy e = Instantiate<Enemy>(enemyPrefab, this.transform);
        e.Init(enemyHP, id, enemyMovePath);
        return e;
    }

    public async UniTask SpawnEnemies(int amount, int delayPerSpawn)
    {
        for (int i = 0; i < amount; i++)
        {
            Enemy e = SpawnEnemy(i);
            e.StartMove();
            enemyList.Add(e);
            await UniTask.Delay(delayPerSpawn);
        }
    }

    private void UpdateEnemyList(object param = null)
    {
        enemyList.Remove((Enemy)param);
    }
}
