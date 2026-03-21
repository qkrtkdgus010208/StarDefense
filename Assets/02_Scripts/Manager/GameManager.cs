using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private StageManager stageManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private HeroManager heroManager;

    [SerializeField] private GameOverUI gameOverUI;

    [field:SerializeField] public PlayerStatus PlayerStatus { get; private set; }

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

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.Show();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
