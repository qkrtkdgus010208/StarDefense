using TMPro;
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

    [SerializeField] StageManager stageManager;
    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] private Button pauseBtn;
    private bool isPaused = false;

    private void Start()
    {
        missonButton.onClick.AddListener(OnMissionButtonClicked);
        shipButton.onClick.AddListener(OnShipButtonClicked);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        stageManager.OnWaveCleared += UpdateWaveText;
        pauseBtn.onClick.AddListener(TogglePause);
    }

    private void OnDestroy()
    {
        missonButton.onClick.RemoveListener(OnMissionButtonClicked);
        shipButton.onClick.RemoveListener(OnShipButtonClicked);
        upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        stageManager.OnWaveCleared -= UpdateWaveText;
        pauseBtn.onClick.RemoveListener(TogglePause);
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

    private void UpdateWaveText()
    {
        waveText.text = $"Wave {stageManager.CurrentWaveIndex + 1}/{stageManager.StageData.waves.Length}";
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
