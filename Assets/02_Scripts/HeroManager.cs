using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private GameObject[] heroPrefab;

    private GameManager gameManager;

    private List<Hero> heroes;

    public void Init(GameManager gm)
    {
        gameManager = gm;
        heroes = new List<Hero>();
    }

    public void SpawnHero(Tilemap map, Vector3Int pos)
    {
        if (heroPrefab.Length == 0) return;

        int randomIndex = Random.Range(0, heroPrefab.Length);
        GameObject heroObj = Instantiate(heroPrefab[randomIndex], map.GetCellCenterWorld(pos), Quaternion.identity);
        Hero hero = heroObj.GetComponent<Hero>();
        heroes.Add(hero);
    }
}
