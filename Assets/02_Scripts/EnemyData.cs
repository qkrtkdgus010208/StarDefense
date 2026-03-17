using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float hp = 20;
    public float moveSpeed = 1f;
    public float atk = 0f;
    public float def = 0f;
}
