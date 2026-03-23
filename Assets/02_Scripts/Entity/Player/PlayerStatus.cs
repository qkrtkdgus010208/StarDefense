using System;
using UnityEngine;

public class PlayerStatus : HeroStatus
{
    [SerializeField] private ShipManager shipManager;

    public int gold;
    public int gem;
    public int maxShip;
    public int currentShip;

    public event Action<int> OnGoldChanged;
    public event Action<int> OnGemChanged;
    public event Action<int, int> OnShipChanged;

    protected override void SetUp()
    {
        base.SetUp();

        gold = heroInfo.startGold;
        gem = heroInfo.gem;
        maxShip = heroInfo.maxShip;
        currentShip = heroInfo.currentShip;

        ReBuild();
    }

    public void ReBuild()
    {
        OnGoldChanged?.Invoke(gold);
        OnGemChanged?.Invoke(gem);
        OnShipChanged?.Invoke(currentShip, maxShip);
    }

    private void Start()
    {
        OnDie += GameManager.Instance.GameOver;
    }

    private void OnDestroy()
    {
       OnDie -= GameManager.Instance.GameOver;
    }

    public void AddGold(int value)
    {
        gold += value;
        OnGoldChanged?.Invoke(gold);
    }

    public bool UseGold(int value)
    {
        if (gold >= value)
        {
            gold -= value;
            OnGoldChanged?.Invoke(gold);
            return true;
        }

        return false;
    }

    public void AddGem(int value)
    {
        gem += value;
        OnGemChanged?.Invoke(gem);
    }

    public bool UseGem(int value)
    {
        if (gem >= value)
        {
            gem -= value;
            OnGemChanged?.Invoke(gem);
            return true;
        }

        return false;
    }

    public bool AddShip(int value)
    {
        if (gold >= value && currentShip < maxShip)
        {
            gold -= value;
            currentShip++;
            shipManager.CreateShip(this);
            OnShipChanged?.Invoke(currentShip, maxShip);
            OnGoldChanged?.Invoke(gold);
            return true;
        }

        return false;
    }
}
