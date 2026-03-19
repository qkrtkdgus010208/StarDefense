using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private TileBase normalTile;
    [SerializeField] private TileBase fixTile;
    [SerializeField] private TileUI tileUI;
    [SerializeField] private HeroUI heroUI;
    [SerializeField] private LayerMask heroLayer;

    private GameManager gameManager;
    private HeroManager heroManager;

    private Camera mainCamara;

    public void Init(GameManager gm, HeroManager hm)
    {
        gameManager = gm;
        heroManager = hm;
        mainCamara = Camera.main;
        tileUI.Init(this);
        heroUI.Init(this);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -mainCamara.transform.position.z;
            Vector3 worldPos = mainCamara.ScreenToWorldPoint(mousePos);

            if (Physics2D.Raycast(worldPos, Vector2.zero) is RaycastHit2D hit && hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Hero hero))
                {
                    CheckHero(hero);
                    return;
                }

                if (hit.collider.TryGetComponent(out Tilemap map))
                {
                    Vector3Int cellPos = map.WorldToCell(worldPos);
                    CheckTile(map, cellPos);
                    return;
                }

            }
            else
            {
                tileUI.Hide();
                heroUI.Hide();
            }
        }
    }

    private void CheckTile(Tilemap map, Vector3Int pos)
    {
        CustomTile tile = map.GetTile<CustomTile>(pos);
        if (tile == null)
        {
            tileUI.Hide();
            heroUI.Hide();
            return;
        }

        Vector3 centerWorldPos = map.GetCellCenterWorld(pos);

        tileUI.Show(tile, map, pos, centerWorldPos);
    }

    private void CheckHero(Hero hero)
    {
        heroUI.Show(hero);
    }

    public void RepairTile(Tilemap map, Vector3Int pos)
    {
        map.SetTile(pos, normalTile);
    }

    public void SpawnHero(Tilemap map, Vector3Int pos)
    {
        Vector3 centerWorldPos = map.GetCellCenterWorld(pos);

        if (Physics2D.OverlapPoint(centerWorldPos, heroLayer) != null) return;

        heroManager.SpawnHero(map, pos);
    }
}
