using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private GameObject[] heroPrefab;
    [SerializeField] private int needMergeCount;

    [SerializeField] private Hero player;

    private GameManager gameManager;

    private List<Hero> heroes;
    private List<Hero> mergeCandidates;
    private List<Hero> destroyCandidates;
    private int targetId;
    private HeroGrade targetTier;

    public void Init(GameManager gm)
    {
        gameManager = gm;
        heroes = new List<Hero>();
        mergeCandidates = new List<Hero>();
        destroyCandidates = new List<Hero>();

        heroes.Add(player);
    }

    public Hero SpawnHero(Tilemap map, Vector3Int pos)
    {
        if (heroPrefab.Length == 0) return null;

        int randomIndex = Random.Range(0, heroPrefab.Length);
        GameObject heroObj = Instantiate(heroPrefab[randomIndex], map.GetCellCenterWorld(pos), Quaternion.identity);
        Hero hero = heroObj.GetComponent<Hero>();
        heroes.Add(hero);
        return hero;
    }

    public bool MergeCheck(Hero hero)
    {
        targetId = hero.CurrentData.id;
        targetTier = hero.CurrentData.grade;

        mergeCandidates = heroes
            .Where(h => h.CurrentData.id == targetId && h.CurrentData.grade == targetTier)
            .ToList();

        return mergeCandidates.Count >= needMergeCount;
    }

    public void PerformMerge(Hero hero)
    {
        foreach (Hero target in mergeCandidates)
        {
            if (target == hero) continue;

            destroyCandidates.Add(target);
        }

        hero.Merge();
        DestroyCandidates();
    }

    private void DestroyCandidates()
    {
        for (int i = 0; i < needMergeCount - 1; i++)
        {
            Hero hero = destroyCandidates[i];
            destroyCandidates.Remove(hero);
            heroes.Remove(hero);
            Destroy(hero.gameObject);
        }

        mergeCandidates.Clear();
        destroyCandidates.Clear();
    }

    public void PerformBeyond(Hero hero)
    {
        hero.Beyond();
    }

    public void Upgrade(HeroGrade grade)
    {
        List<Hero> targetHeroes = heroes.Where(h => h.CurrentData.grade == grade).ToList();

        foreach (var hero in targetHeroes)
        {
            hero.ApplyUpgrade();
        }
    }
}
