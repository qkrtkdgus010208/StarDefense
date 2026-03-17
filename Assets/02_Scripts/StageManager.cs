using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] public Transform[] wayPoints;

    private GameManager gameManager;
    private EnemyManager enemyManager;

    public void Init(GameManager gm, EnemyManager em)
    {
        gameManager = gm;
        enemyManager = em;
    }

    public void Setup()
    {
        enemyManager.Setup(stageData, wayPoints);
    }
}
