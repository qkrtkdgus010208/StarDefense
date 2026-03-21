using TMPro;
using UnityEngine;

public class HUDUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI shipText;

    [SerializeField] private PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus.OnGoldChanged += UpdateGoldText;
        playerStatus.OnGemChanged += UpdateGemText;
        playerStatus.OnShipChanged += UpdateShipText;

        playerStatus.ReBuild();
    }

    private void OnDestroy()
    {
        playerStatus.OnGoldChanged -= UpdateGoldText;
        playerStatus.OnGemChanged -= UpdateGemText;
        playerStatus.OnShipChanged -= UpdateShipText;
    }

    private void UpdateGoldText(int gold)
    {
        goldText.text = $"{gold}G";
    }
    private void UpdateGemText(int gem)
    {
        gemText.text = $"{gem}잼";
    }
    private void UpdateShipText(int currentShip, int maxShip)
    {
        shipText.text = $"{currentShip}/{maxShip}";
    }
}
