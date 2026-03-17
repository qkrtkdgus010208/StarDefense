using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private StageManager stageManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private HeroManager heroManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        stageManager.Init(this, enemyManager);
        enemyManager.Init(this, stageManager);
        tileManager.Init(this, heroManager);
        heroManager.Init(this);
    }

    private void Start()
    {
        stageManager.Setup();
    }
}
