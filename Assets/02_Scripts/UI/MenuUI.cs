using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button missonButton;
    [SerializeField] private Button shipButton;
    [SerializeField] private Button upgradeButton;

    [SerializeField] MissonUI missonUI;
    [SerializeField] ExplorerShipUI shipUI;
    [SerializeField] UpgradeUI upgradeUI;

    private void Start()
    {
        missonButton.onClick.AddListener(OnMissionButtonClicked);
        shipButton.onClick.AddListener(OnShipButtonClicked);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
    }

    private void OnDestroy()
    {
        missonButton.onClick.RemoveListener(OnMissionButtonClicked);
        shipButton.onClick.RemoveListener(OnShipButtonClicked);
        upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
    }

    private void OnMissionButtonClicked()
    {
        missonUI.Show();
    }

    private void OnShipButtonClicked()
    {
        shipUI.Show();
    }

    private void OnUpgradeButtonClicked()
    {
        upgradeUI.Show();
    }
}
