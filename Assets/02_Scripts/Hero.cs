using TMPro;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TextMeshProUGUI tier;
    [SerializeField] private LayerMask targetLayer;

    private HeroInfo currentData;
    private float attackTimer;
    private Transform currentTarget;

    private void Awake()
    {
        currentData = heroData.heroInfos[0];
        tier.text = currentData.tier.ToString();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (currentTarget == null || Vector3.Distance(transform.position, currentTarget.position) > currentData.range)
        {
            currentTarget = ScanTarget();
        }

        if (currentTarget != null && attackTimer >= 1f / currentData.attackRate)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    private Transform ScanTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, currentData.range, targetLayer);

        Transform nearestEnemy = null;
        float minDistance = currentData.range;

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
            projectile.Init(currentTarget, currentData.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (currentData == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentData.range);
    }
}
