using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeUI : MonoBehaviour
{
    [SerializeField] private GameObject popupRoot; // UI 부모 오브젝트
    [SerializeField] private Button mergeButton;

    private TileManager tileManager;
    private HeroManager heroManager;

    private Hero currentHero;

    public void Init(TileManager tileManager, HeroManager heroManager)
    {
        this.tileManager = tileManager;
        this.heroManager = heroManager;
        Hide();
    }

    private void Awake()
    {
        mergeButton.onClick.AddListener(OnActionMergeButtonClicked);
    }

    private void OnDestroy()
    {
        mergeButton.onClick.RemoveListener(OnActionMergeButtonClicked);
    }

    private void OnActionMergeButtonClicked()
    {
        heroManager.PerformMerge(currentHero);
        Hide();
    }

    public void Show(Hero hero)
    {
        currentHero = hero;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(hero.transform.position);
        transform.position = screenPos;

        popupRoot.SetActive(true);
    }

    public void Hide()
    {
        popupRoot.SetActive(false);
    }
}
