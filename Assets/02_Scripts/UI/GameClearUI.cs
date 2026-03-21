using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;

    private void Start()
    {
        retryButton.onClick.AddListener(OnRetryButtonClicked);
    }

    private void OnDestroy()
    {
        retryButton.onClick.RemoveListener(OnRetryButtonClicked);
    }

    private void OnRetryButtonClicked()
    {
        GameManager.Instance.RestartGame();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
