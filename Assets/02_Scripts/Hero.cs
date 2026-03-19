using TMPro;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private HeroStatus status;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TextMeshProUGUI tier;
    [SerializeField] private LayerMask targetLayer;

    public HeroInfo CurrentData { get; private set; }
    private int currentIndex = 0;
    private float attackTimer;
    private Transform currentTarget;

    public bool IsMaxTier => currentIndex + 1 >= heroData.heroInfos.Length;

    private void Awake()
    {
        CurrentData = heroData.heroInfos[currentIndex];
        if (tier != null)
            tier.text = CurrentData.tier.ToString();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (currentTarget == null || Vector3.Distance(transform.position, currentTarget.position) > CurrentData.range)
        {
            currentTarget = ScanTarget();
        }

        if (currentTarget != null && attackTimer >= 1f / CurrentData.attackRate)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    private Transform ScanTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, CurrentData.range, targetLayer);

        Transform nearestEnemy = null;
        float minDistance = CurrentData.range;

        foreach (var col in targets)
        {
            if (col.TryGetComponent(out Enemy enemy))
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy.transform;
                }
            }
        }
        return nearestEnemy;
    }

    private void Attack()
    {
        GameObject obj = Instantiate(heroData.projectilePrefab, firePoint.position, Quaternion.identity);
        if (obj.TryGetComponent(out Projectile projectile))
        {
            projectile.Init(currentTarget, CurrentData.atk);
        }
    }

    public void Merge()
    {
        if (IsMaxTier)
        {
            // TODO: 초월 시스템 구현해야됨
            Debug.Log($"최종 등급입니다!");
            return;
        }

        currentIndex++;
        HeroInfo nextInfo = heroData.heroInfos[currentIndex];
        CurrentData = nextInfo;

        if (tier != null)
            tier.text = CurrentData.tier.ToString();

        status.UpdateStat();
    }

    private void OnDrawGizmosSelected()
    {
        if (CurrentData == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, CurrentData.range);
    }
}
