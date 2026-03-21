using UnityEngine;

public class Enemy : Entity
{
    [field:SerializeField] public EnemyData EnemyData { get; private set; }

    private GameManager gameManager;
    private EnemyManager enemyManager;

    private Transform[] wayPoints;
    private int currentIndex = 0;

    public void Init(Transform[] paths, GameManager gm, EnemyManager em)
    {
        Init();

        gameManager = gm;
        enemyManager = em;
        wayPoints = paths;

        Status.OnDie += Die;

        currentIndex = 0;

        // 시작 위치로 이동
        if (wayPoints != null && wayPoints.Length > 0)
            transform.position = wayPoints[currentIndex].position;
    }

    private void OnDestroy()
    {
        Status.OnDie -= Die;
    }

    private void Update()
    {
        if (wayPoints == null || currentIndex >= wayPoints.Length) return;

        MoveToNextPoint();
    }

    private void Die()
    {
        gameManager.PlayerStatus.AddGold(EnemyData.rewardGold);
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }

    private void MoveToNextPoint()
    {
        // 현재 목표 지점
        Vector3 targetPos = wayPoints[currentIndex].position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, EnemyData.moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            currentIndex++;

            // 마지막 지점에 도달했을 때 처리
            if (currentIndex >= wayPoints.Length)
            {
                OnReachEndPoint();
            }
        }
    }

    private void OnReachEndPoint()
    {
        gameManager.PlayerStatus.TakeHp(EnemyData.atk);
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
