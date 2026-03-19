using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private GameObject[] heroPrefab;

    private GameManager gameManager;

    private List<Hero> heroes;
    private List<Hero> mergeCandidates;
    private int targetId;
    private int targetTier;

    public void Init(GameManager gm)
    {
        gameManager = gm;
        heroes = new List<Hero>();
        mergeCandidates = new List<Hero>();
    }

    public void SpawnHero(Tilemap map, Vector3Int pos)
    {
        if (heroPrefab.Length == 0) return;

        int randomIndex = Random.Range(0, heroPrefab.Length);
        GameObject heroObj = Instantiate(heroPrefab[randomIndex], map.GetCellCenterWorld(pos), Quaternion.identity);
        Hero hero = heroObj.GetComponent<Hero>();
        heroes.Add(hero);
    }

    public bool MergeCheck(Hero hero)
    {
        targetId = hero.CurrentData.id;
        targetTier = hero.CurrentData.tier;

        mergeCandidates = heroes
            .Where(h => h.CurrentData.id == targetId && h.CurrentData.tier == targetTier)
            .Take(3)
            .ToList();

        return mergeCandidates.Count >= 3;
    }

    public void PerformMerge(Hero hero)
    {
        foreach (var target in mergeCandidates)
        {
            if (target == hero) continue;

            heroes.Remove(target);
            Destroy(target.gameObject);
        }

        hero.Merge();
    }
}
