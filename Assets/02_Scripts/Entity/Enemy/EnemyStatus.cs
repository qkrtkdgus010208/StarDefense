
public class EnemyStatus : Status
{
    private Enemy enemy;
    private EnemyData enemyData;

    protected override void SetUp()
    {
        enemy = GetComponent<Enemy>();
        enemyData = enemy.EnemyData;

        MaxHp = enemyData.hp;
        CurrentHp = enemyData.hp;
        MaxMp = 0f;
        CurrentMp = 0f;
        CurrentAtk = enemyData.atk;
        CurrentDef = enemyData.def;
        CriticalRate = 0f;
        EvasionRate = 0f;
    }
}
