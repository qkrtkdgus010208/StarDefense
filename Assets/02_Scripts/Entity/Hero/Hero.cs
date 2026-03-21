using TMPro;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] public HeroData heroData;
    [SerializeField] private HeroStatus status;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TextMeshProUGUI tier;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float upgradeMultiplier = 1f;

    public HeroInfo CurrentData { get; private set; }
    private int currentIndex = 0;

    public bool HasBuff { get; private set; }
    public float AdditionalAttackRate { get; private set; }

    private float attackTimer;
    private Transform currentTarget;

    public bool IsMaxTier => CurrentData.canBeyond;
    public bool IsBeyond => CurrentData.isBeyond;

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

        if (currentTarget != null && attackTimer >= 1f / (CurrentData.attackRate + AdditionalAttackRate))
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
            float finalAtk = CurrentData.atk * upgradeMultiplier;
            projectile.Init(currentTarget, finalAtk);
        }
    }

    public void Merge()
    {
        if (IsMaxTier) return;
        GetNextData();

        if (tier != null)
            tier.text = CurrentData.tier.ToString();

        status.UpdateStat();
    }

    private void GetNextData()
    {
        currentIndex++;
        HeroInfo nextInfo = heroData.heroInfos[currentIndex];
        CurrentData = nextInfo;
    }

    public void Beyond()
    {
        if (IsMaxTier)
        {
            GetNextData();

            if (tier != null)
                tier.text = "X";

            status.UpdateStat();
        }
    }

    public void ApplyBuff()
    {
        if (HasBuff) return;

        HasBuff = true;
        AdditionalAttackRate = CurrentData.attackRate;
    }

    public void RemoveBuff()
    {
        HasBuff = false;
        AdditionalAttackRate = 0f;
    }

    public void ApplyUpgrade()
    {
        upgradeMultiplier += 0.2f;
        status.UpdateStat();
    }

    private void OnDrawGizmosSelected()
    {
        if (CurrentData == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, CurrentData.range);
    }
}
