using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExplorerShipUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button summonBtn;
    [SerializeField] private Button closeBtn;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI summonCostText;

    [Header("etc")]
    [SerializeField] private PlayerStatus playerStatus;

    private int summonLevel = 1;

    private void Start()
    {
        summonBtn.onClick.AddListener(OnSummonBtnClicked);
        closeBtn.onClick.AddListener(CloseBtnClicked);

        UpdateView();
    }

    private void OnDestroy()
    {
        summonBtn.onClick.RemoveListener(OnSummonBtnClicked);
        closeBtn.onClick.RemoveListener(CloseBtnClicked);
    }

    private void OnSummonBtnClicked()
    {
        if (playerStatus.AddShip(summonLevel * 20))
        {
            summonLevel++;
            UpdateView();
        }
    }

    private void UpdateView()
    {
        summonCostText.text = $"{summonLevel * 20}G";
    }

    private void CloseBtnClicked()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
