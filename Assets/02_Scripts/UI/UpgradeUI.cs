using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [Header("Upgrade Buttons")]
    [SerializeField] private Button normalUpgradeBtn;
    [SerializeField] private Button rareUpgradeBtn;
    [SerializeField] private Button epicUpgradeBtn;
    [SerializeField] private Button playerUpgradeBtn;

    [Header("Upgrade Level Texts")]
    [SerializeField] private TextMeshProUGUI normalUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI rareUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI epicUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI playerUpgradeLevelText;

    [Header("Upgrade Cost Texts")]
    [SerializeField] private TextMeshProUGUI normalUpgradeCost;
    [SerializeField] private TextMeshProUGUI rareUpgradeCost;
    [SerializeField] private TextMeshProUGUI epicUpgradeCost;
    [SerializeField] private TextMeshProUGUI playerUpgradeCost;

    [Header("Close Button")]
    [SerializeField] private Button closeBtn;

    [Header("etc")]
    [SerializeField] private HeroManager heroManager;
    [SerializeField] private PlayerStatus playerStatus;

    private int normalUpgradeLevel = 1;
    private int rareUpgradeLevel = 1;
    private int epicUpgradeLevel = 1;
    private int playerUpgradeLevel = 1;

    private void Start()
    {
        normalUpgradeBtn.onClick.AddListener(OnnormalUpgradeBtnClicked);
        rareUpgradeBtn.onClick.AddListener(OnrareUpgradeBtnClicked);
        epicUpgradeBtn.onClick.AddListener(OnepicUpgradeBtnClicked);
        playerUpgradeBtn.onClick.AddListener(OnplayerUpgradeBtnClicked);
        closeBtn.onClick.AddListener(closeBtnClicked);

        UpdateView();
    }

    private void OnDestroy()
    {
        normalUpgradeBtn.onClick.RemoveListener(OnnormalUpgradeBtnClicked);
        rareUpgradeBtn.onClick.RemoveListener(OnrareUpgradeBtnClicked);
        epicUpgradeBtn.onClick.RemoveListener(OnepicUpgradeBtnClicked);
        playerUpgradeBtn.onClick.RemoveListener(OnplayerUpgradeBtnClicked);
        closeBtn.onClick.RemoveListener(closeBtnClicked);
    }

    private void UpdateView()
    {
        normalUpgradeLevelText.text = $"Lv.{normalUpgradeLevel - 1}";
        rareUpgradeLevelText.text = $"Lv.{rareUpgradeLevel - 1}";
        epicUpgradeLevelText.text = $"Lv.{epicUpgradeLevel - 1}";
        playerUpgradeLevelText.text = $"Lv.{playerUpgradeLevel - 1}";

        normalUpgradeCost.text = $"{normalUpgradeLevel * 10}잼";
        rareUpgradeCost.text = $"{rareUpgradeLevel * 20}잼";
        epicUpgradeCost.text = $"{epicUpgradeLevel * 30}잼";
        playerUpgradeCost.text = $"{playerUpgradeLevel * 50}G";
    }

    private void OnnormalUpgradeBtnClicked()
    {
        if (playerStatus.UseGem(normalUpgradeLevel * 10))
        {
            heroManager.Upgrade(HeroGrade.Normal);
            normalUpgradeLevel++;
            UpdateView();
        }
    }

    private void OnrareUpgradeBtnClicked()
    {
        if (playerStatus.UseGem(rareUpgradeLevel * 20))
        {
            heroManager.Upgrade(HeroGrade.Rare);
            rareUpgradeLevel++;
            UpdateView();
        }
    }

    private void OnepicUpgradeBtnClicked()
    {
        if (playerStatus.UseGem(epicUpgradeLevel * 30))
        {
            heroManager.Upgrade(HeroGrade.Epic);
            epicUpgradeLevel++;
            UpdateView();
        }
    }

    private void OnplayerUpgradeBtnClicked()
    {
        if (playerStatus.UseGold(playerUpgradeLevel * 50))
        {
            heroManager.Upgrade(HeroGrade.Commander);
            playerUpgradeLevel++;
            UpdateView();
        }
    }

    private void closeBtnClicked()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
