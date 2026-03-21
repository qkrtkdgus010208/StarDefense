using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private Transform[] wayPoints;

    private GameManager gameManager;
    private EnemyManager enemyManager;

    private WaveInfo currentWaveInfo;
    private int currentWaveIndex = 0;
    [field: SerializeField] public bool IsSpawnFinished { get; private set; }

    public void Init(GameManager gm, EnemyManager em)
    {
        gameManager = gm;
        enemyManager = em;

        currentWaveInfo = stageData.waves[currentWaveIndex];
    }

    public void Setup()
    {
        IsSpawnFinished = false;
        enemyManager.Setup(currentWaveInfo, wayPoints);
    }

    public void NextWaveStart()
    {
        if (currentWaveIndex >= stageData.waves.Length - 1)
        {
            IsSpawnFinished = true;
            return;
        }

        currentWaveInfo = stageData.waves[++currentWaveIndex];
        enemyManager.SetupNext(currentWaveInfo);
    }
}
