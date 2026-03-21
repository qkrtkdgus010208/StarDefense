using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeyoundUI : MonoBehaviour
{
    [SerializeField] private GameObject popupRoot; // UI 부모 오브젝트
    [SerializeField] private Button beyoundButton;
    [SerializeField] private TextMeshProUGUI costText;

    private TileManager tileManager;
    private HeroManager heroManager;
    private PlayerStatus playerStatus;
    
    private Hero currentHero;

    public void Init(TileManager tileManager, HeroManager heroManager, PlayerStatus playerStatus)
    {
        this.tileManager = tileManager;
        this.heroManager = heroManager;
        this.playerStatus = playerStatus;
        Hide();
    }

    private void Awake()
    {
        beyoundButton.onClick.AddListener(OnActionBeyoundButtonClicked);
    }

    private void OnDestroy()
    {
        beyoundButton.onClick.RemoveListener(OnActionBeyoundButtonClicked);
    }

    private void OnActionBeyoundButtonClicked()
    {
        if (!playerStatus.UseGem(currentHero.CurrentData.beyondCost)) return;

        heroManager.PerformBeyond(currentHero);
        Hide();
    }

    public void Show(Hero hero)
    {
        currentHero = hero;
        costText.text = hero.CurrentData.beyondCost.ToString();

        Vector3 screenPos = Camera.main.WorldToScreenPoint(hero.transform.position);
        transform.position = screenPos;

        popupRoot.SetActive(true);
    }

    public void Hide()
    {
        popupRoot.SetActive(false);
    }
}
