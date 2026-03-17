using System;
using UnityEngine;

public abstract class Status : MonoBehaviour
{
    public float MaxHp { get; protected set; }
    public float CurrentHp { get; protected set; }
    public float MaxMp { get; protected set; }
    public float CurrentMp { get; protected set; }
    public float CurrentAtk { get; protected set; }
    public float CurrentDef { get; protected set; }
    public float CriticalRate { get; protected set; }
    public float EvasionRate { get; protected set; }

    public event Action<float, float> OnHpChanged;
    public event Action<float, float> OnMpChanged;
    public event Action OnDie;

    public void Init()
    {
        SetUp();
    }

    protected abstract void SetUp();

    public void TakeHp(float value)
    {
        CurrentHp -= value;
        OnHpChanged?.Invoke(CurrentHp, MaxHp);

        if (CurrentHp < 1)
        {
            OnDie?.Invoke();
        }
    }

    public void HealHp(float value)
    {
        float beforeHp = CurrentHp;

        CurrentHp = Mathf.Min(CurrentHp + value, MaxHp);

        float afterHp = CurrentHp;

        float deltaHp = afterHp - beforeHp;

        if (deltaHp <= 0)
            return;

        //GameManager.Instance.CombatManager.HealPopUp(deltaHp);

        OnHpChanged?.Invoke(CurrentHp, MaxHp);
    }

    public void TakeMp(float value)
    {
        if (CurrentMp >= value)
        {
            CurrentMp -= value;
            OnMpChanged?.Invoke(CurrentMp, MaxMp);
        }
    }

    public void HealMp(float value)
    {
        CurrentMp = Mathf.Min(CurrentMp + value, MaxMp);
        OnMpChanged?.Invoke(CurrentMp, MaxMp);
    }

    public void ResetPlayerHpMp()
    {
        CurrentHp = MaxHp;
        CurrentMp = MaxMp;
        OnMpChanged?.Invoke(CurrentMp, MaxMp);
        OnHpChanged?.Invoke(CurrentHp, MaxHp);
    }
}
