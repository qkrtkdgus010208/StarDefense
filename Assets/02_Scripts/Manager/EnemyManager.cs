using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;
    private StageManager stageManager;

    private StageData stageData;
    private Transform[] wayPoints;

    private Coroutine spawnCoroutine;
    private List<Enemy> enemies;

    public void Init(GameManager gm, StageManager sm)
    {
        gameManager = gm;
        stageManager = sm;

        enemies = new List<Enemy>();
    }

    public void Setup(StageData stageData, Transform[] wayPoints)
    {
        this.stageData = stageData;
        this.wayPoints = wayPoints;

        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (var wave in stageData.waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject enemyObj = Instantiate(wave.enemyPrefab);
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                enemy.Init(wayPoints, gameManager);
                enemies.Add(enemy);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            yield return new WaitForSeconds(wave.waitAfterWave);
        }
    }
}
