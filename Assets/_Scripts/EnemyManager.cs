using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyManager : SingletonMonobehaviour<EnemyManager>
{
    [SerializeField] private Enemy[] enemyPrefabs;
    public List<Enemy> enemyList;
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
    public void Init(int totalEnemy)
    {
        enemyMovePath = GameManager.instance.mainPathPoints;
        enemyList = new List<Enemy>();
        // SpawnEnemies(totalEnemy);
    }

    private Enemy SpawnEnemy(int id, Enemy enemyPrefab = null)
    {
        if (!enemyPrefab) enemyPrefab = enemyPrefabs.PickRandom();
        Enemy e = Instantiate<Enemy>(enemyPrefab, this.transform);
        e.Init(enemyHP, id, enemyMovePath);
        return e;
    }

    public void SpawnEnemies(int amount, Enemy enemyPrefab = null)
    {
        for (int i = 0; i < amount; i++)
        {
            Enemy e = SpawnEnemy(i, enemyPrefab);
            enemyList.Add(e);
        }
    }

    private void UpdateEnemyList(object param = null)
    {
        enemyList.Remove((Enemy)param);
    }

    public Enemy GetEnemy()
    {
        if (enemyList.Count == 0) return null;
        Enemy enemyToGet = enemyList[enemyList.Count - 1];
        enemyList.Remove(enemyToGet);
        return enemyToGet;
    }

    public void EnemiesStartWave(int amountEnemyOfWave, int delayPerEnemy)
    {
        SpawnEnemies(amountEnemyOfWave);

        for (int i = 0; i < amountEnemyOfWave; i++)
        {
            _ = GetEnemy().StartMove(delayPerEnemy * i);
        }
    }
}
