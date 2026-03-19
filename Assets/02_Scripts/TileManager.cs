using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private TileBase normalTile;
    [SerializeField] private TileBase fixTile;
    
    [SerializeField] private LayerMask heroLayer;
    [SerializeField] private LayerMask tileLayer;

    [SerializeField] private PlayerStatus playerStatus;

    [SerializeField] private TileUI tileUI;
    [SerializeField] private HeroUI heroUI;
    [SerializeField] private MergeUI mergeUI;
    [SerializeField] private BeyoundUI beyondUI;

    private GameManager gameManager;
    private HeroManager heroManager;

    private CustomTile currentCustomTile;

    private Camera mainCamara;

    public void Init(GameManager gm, HeroManager hm)
    {
        gameManager = gm;
        heroManager = hm;
        mainCamara = Camera.main;
        tileUI.Init(this);
        heroUI.Init(this);
        mergeUI.Init(this, heroManager);
        beyondUI.Init(this, heroManager);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -mainCamara.transform.position.z;
            Vector3 worldPos = mainCamara.ScreenToWorldPoint(mousePos);

            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);

            if (hits.Length > 0)
            {
                Hero foundHero = null;
                Tilemap foundMap = null;

                foreach (var hit in hits)
                {
                    if (hit.collider.TryGetComponent(out Hero hero))
                    {
                        foundHero = hero;
                    }
                    else if (hit.collider.TryGetComponent(out Tilemap map))
                    {
                        foundMap = map;
                    }
                }

                if (foundHero != null)
                {
                    CheckHero(foundHero);
                    return;
                }

                if (foundMap != null)
                {
                    Vector3Int cellPos = foundMap.WorldToCell(worldPos);
                    CheckTile(foundMap, cellPos);
                }
            }
            else
            {
                AllHide();
            }
        }
    }

    private void CheckTile(Tilemap map, Vector3Int pos)
    {
        currentCustomTile = map.GetTile<CustomTile>(pos);
        if (currentCustomTile == null)
        {
            AllHide();
            return;
        }

        Vector3 centerWorldPos = map.GetCellCenterWorld(pos);

        AllHide();
        tileUI.Show(currentCustomTile, map, pos, centerWorldPos);
    }

    private void CheckHero(Hero hero)
    {
        AllHide();

        if (hero.IsMaxTier)
        {
            beyondUI.Show(hero);
            heroUI.Show(hero);
            return;
        }

        if (heroManager.MergeCheck(hero))
        {
            mergeUI.Show(hero);
            heroUI.Show(hero);
            return;
        }
    }

    public void RepairTile(Tilemap map, Vector3Int pos)
    {
        if (!playerStatus.UseGold(currentCustomTile.cost)) return;

        map.SetTile(pos, normalTile);
        currentCustomTile = null;
    }

    public void SpawnHero(Tilemap map, Vector3Int pos)
    {
        if (!playerStatus.UseGold(currentCustomTile.cost)) return;

        Vector3 centerWorldPos = map.GetCellCenterWorld(pos);

        if (Physics2D.OverlapPoint(centerWorldPos, heroLayer) != null) return;

        heroManager.SpawnHero(map, pos);

        currentCustomTile = null;
    }

    private void AllHide()
    {
        tileUI.Hide();
        heroUI.Hide();
        mergeUI.Hide();
        beyondUI.Hide();
    }
}
