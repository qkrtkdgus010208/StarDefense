using TMPro;
using UnityEngine;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private GameObject popupRoot; // UI 부모 오브젝트
    [SerializeField] private TextMeshProUGUI tierText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI attackRateText;
    [SerializeField] private TextMeshProUGUI damageText;

    private TileManager tileManager;
    private HeroInfo heroInfo;

    public void Init(TileManager tileManager)
    {
        this.tileManager = tileManager;
        Hide();
    }

    public void Show(Hero hero)
    {
        heroInfo = hero.CurrentData;

        tierText.text = heroInfo.grade.ToString();
        nameText.text = heroInfo.name;
        descText.text = heroInfo.description;
        rangeText.text = heroInfo.range.ToString();

        float finalAttackRate = heroInfo.attackRate;

        if (hero.HasBuff)
        {
            finalAttackRate += hero.AdditionalAttackRate;

            attackRateText.text = $"{finalAttackRate} (+{hero.AdditionalAttackRate})";
            attackRateText.color = Color.green;
        }
        else
        {
            attackRateText.text = finalAttackRate.ToString();
            attackRateText.color = Color.white;
        }

        popupRoot.SetActive(true);
    }

    public void Hide()
    {
        popupRoot.SetActive(false);
    }
}
