using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileUI : MonoBehaviour
{
    [SerializeField] private GameObject popupRoot; // UI 부모 오브젝트
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI costText;

    private TileManager tileManager;
    private CustomTile customTile;
    private Tilemap currentMap;
    private Vector3Int currentCellPos;

    public void Init(TileManager tileManager)
    {
        this.tileManager = tileManager;
        Hide();
    }

    private void Awake()
    {
        actionButton.onClick.AddListener(OnActionButtonClicked);
    }

    private void OnDestroy()
    {
        actionButton.onClick.RemoveListener(OnActionButtonClicked);
    }

    private void OnActionButtonClicked()
    {
        if (customTile.isFix)
        {
            tileManager.RepairTile(currentMap, currentCellPos);
        }
        else if (customTile.isBuff)
        {
            Hero hero = tileManager.SpawnHero(currentMap, currentCellPos);
            if (hero != null)
                hero.ApplyBuff();
        }
        else
        {
            tileManager.SpawnHero(currentMap, currentCellPos);
        }
        Hide();
    }

    public void Show(CustomTile customTile, Tilemap map, Vector3Int pos, Vector3 worldPos)
    {
        this.customTile = customTile;
        this.currentMap = map;
        this.currentCellPos = pos;

        titleText.text = customTile.uiName;
        costText.text = customTile.cost.ToString();

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;
        popupRoot.SetActive(true);
    }

    public void Hide()
    {
        popupRoot.SetActive(false);
    }
}