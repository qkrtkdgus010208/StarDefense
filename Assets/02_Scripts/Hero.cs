using TMPro;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TextMeshProUGUI tier;
    [SerializeField] private LayerMask targetLayer;

    public HeroInfo CurrentData { get; private set; }
    private float attackTimer;
    private Transform currentTarget;

    private void Awake()
    {
        CurrentData = heroData.heroInfos[0];
        if (tier != null)
            tier.text = CurrentData.tier.ToString();
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

    private void OnDrawGizmosSelected()
    {
        if (CurrentData == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, CurrentData.range);
    }
}
