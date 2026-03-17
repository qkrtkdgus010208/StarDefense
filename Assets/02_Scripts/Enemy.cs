using UnityEngine;

public class Enemy : Entity
{
    [field:SerializeField] public EnemyData EnemyData { get; private set; }

    private Transform[] wayPoints;
    private int currentIndex = 0;

    public void Init(Transform[] paths)
    {
        Init();

        Status.OnDie += Die;

        wayPoints = paths;
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
        Destroy(gameObject);
    }

    private void MoveToNextPoint()
    {
        // 현재 목표 지점
        Vector3 targetPos = wayPoints[currentIndex].position;

        // 방향 계산 및 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPos, EnemyData.moveSpeed * Time.deltaTime);

        // 목표 지점에 거의 도달했는지 확인 (부동 소수점 오차 방지)
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
        // 지휘관 체력 감소 로직 호출 (Observer 패턴 활용 권장)
        Destroy(gameObject);
    }
}
