using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [field: SerializeField] public StageData StageData { get; private set; }
    [SerializeField] private Transform[] wayPoints;

    private GameManager gameManager;
    private EnemyManager enemyManager;

    private WaveInfo currentWaveInfo;
    public int CurrentWaveIndex { get; private set; }
    [field: SerializeField] public bool IsSpawnFinished { get; private set; }

    public event Action OnWaveCleared;

    public void Init(GameManager gm, EnemyManager em)
    {
        gameManager = gm;
        enemyManager = em;

        CurrentWaveIndex = 0;
        currentWaveInfo = StageData.waves[CurrentWaveIndex];
    }

    public void Setup()
    {
        IsSpawnFinished = false;
        enemyManager.Setup(currentWaveInfo, wayPoints);
        OnWaveCleared?.Invoke();
    }

    public void NextWaveStart()
    {
        if (CurrentWaveIndex >= StageData.waves.Length - 1)
        {
            IsSpawnFinished = true;
            return;
        }

        currentWaveInfo = StageData.waves[++CurrentWaveIndex];
        enemyManager.SetupNext(currentWaveInfo);
        OnWaveCleared?.Invoke();
    }
}
