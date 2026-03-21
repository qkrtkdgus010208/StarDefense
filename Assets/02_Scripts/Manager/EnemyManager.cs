using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;
    private StageManager stageManager;

    [SerializeField] private WaveInfo currentWaveInfo;
    private Transform[] wayPoints;

    private Coroutine spawnCoroutine;
    private Coroutine waitCoroutine;
    private List<Enemy> enemies;

    private bool isSpawning;

    public int EnemyCount => enemies.Count;

    public void Init(GameManager gm, StageManager sm)
    {
        gameManager = gm;
        stageManager = sm;

        enemies = new List<Enemy>();
    }

    public void Setup(WaveInfo waveInfo, Transform[] wayPoints)
    {
        currentWaveInfo = waveInfo;
        this.wayPoints = wayPoints;

        ReStartCor();
    }

    public void SetupNext(WaveInfo waveInfo)
    {
        currentWaveInfo = waveInfo;
        ReStartCor();
    }

    private void ReStartCor()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnEnemies());

        if (waitCoroutine != null)
            StopCoroutine(waitCoroutine);
        waitCoroutine = StartCoroutine(NextWaveStart());
    }

    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        for (int i = 0; i < currentWaveInfo.count; i++)
        {
            GameObject enemyObj = Instantiate(currentWaveInfo.enemyPrefab);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.Init(wayPoints, gameManager, this);
            enemies.Add(enemy);

            if (i == currentWaveInfo.count)
                isSpawning = false;

            yield return new WaitForSeconds(currentWaveInfo.spawnInterval);
        }
    }

    private IEnumerator NextWaveStart()
    {
        yield return new WaitForSeconds(currentWaveInfo.waitAfterWave);
        stageManager.NextWaveStart();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);

        if (IsWaveClear())
        {
            stageManager.NextWaveStart();
            IsStageClear();
        }
    }

    private bool IsWaveClear()
    {
        return enemies.Count <= 0 && isSpawning;
    }

    private void IsStageClear()
    {
        if (stageManager.IsSpawnFinished)
            gameManager.GameClear();
    }
}
