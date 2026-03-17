using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObjects/HeroData")]
public class HeroData : ScriptableObject
{
    public GameObject heroPrefab;
    public HeroInfo[] heroInfos;   
    public GameObject projectilePrefab;
}

[System.Serializable]
public class HeroInfo
{
    public int tier; // 1, 2, 3 등급
    public float range = 5f;       // 탐지 사거리
    public float attackRate = 1f; // 초당 공격 횟수
    public float damage = 10f;
}
