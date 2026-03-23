using UnityEngine;

public class ExplorerShip : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    private float speed;
    private bool isReturning = false;
    private PlayerStatus playerStatus;

    public void Init(Vector3 start, Vector3 end, float shipSpeed, PlayerStatus status)
    {
        startPos = start;   // 가운데 목표 지점
        targetPos = end;    // 오른쪽 혹은 왼쪽 끝 지점
        speed = shipSpeed;
        playerStatus = status;
        transform.position = startPos;
    }

    private void Update()
    {
        Vector3 currentDestination = isReturning ? startPos : targetPos;

        transform.position = Vector3.MoveTowards(transform.position, currentDestination, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentDestination) < 0.01f)
        {
            if (!isReturning)
            {
                isReturning = true;
            }
            else
            {
                playerStatus.AddGem(10);
                isReturning = false;
            }
        }
    }
}