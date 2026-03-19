
using System;

public class HeroStatus : Status
{
    protected Hero hero;
    protected HeroInfo heroInfo;

    protected override void SetUp()
    {
        hero = GetComponent<Hero>();
        heroInfo = hero.CurrentData;

        MaxHp = heroInfo.hp;
        CurrentHp = heroInfo.hp;
        MaxMp = 0f;
        CurrentMp = 0f;
        CurrentAtk = heroInfo.atk;
        CurrentDef = heroInfo.def;
        CriticalRate = 0f;
        EvasionRate = 0f;
    }

    public void UpdateStat()
    {
        SetUp();
    }
}
