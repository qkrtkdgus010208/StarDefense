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
    public int id;
    public int tier; // 1, 2, 3 등급
    public string name;
    public string description;
    public int beyondCost;
    public float hp = 1000f;
    public float atk = 10f;
    public float def = 0f;
    public float range = 5f;       // 탐지 사거리
    public float attackRate = 1f; // 초당 공격 횟수

    public int startGold = 150;
    public int gem = 0;
    public int maxShip = 20;
    public int currentShip = 0;
}
