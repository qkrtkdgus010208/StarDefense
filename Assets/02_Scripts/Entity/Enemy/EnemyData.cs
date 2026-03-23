using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;
    public string enemyName;
    public float hp = 20;
    public float moveSpeed = 1f;
    public float atk = 0f;
    public float def = 0f;
    public int rewardGold = 10;
}
