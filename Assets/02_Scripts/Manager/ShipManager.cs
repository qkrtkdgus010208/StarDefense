using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private Transform centerPoint; // 가운데 목표 지점
    [SerializeField] private Transform leftPoint;   // 왼쪽 끝 지점
    [SerializeField] private Transform rightPoint;  // 오른쪽 끝 지점
    [SerializeField] private float shipSpeed = 5f;

    private bool nextIsRight = true;

    public void CreateShip(PlayerStatus status)
    {
        Transform endPoint = nextIsRight ? rightPoint : leftPoint;

        GameObject obj = Instantiate(shipPrefab, centerPoint.position, Quaternion.identity);
        if (obj.TryGetComponent(out ExplorerShip ship))
            ship.Init(centerPoint.position, endPoint.position, shipSpeed, status);

        nextIsRight = !nextIsRight;
    }
}