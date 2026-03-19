
public class HeroStatus : Status
{
    private Hero hero;
    private HeroInfo heroData;

    protected override void SetUp()
    {
        hero = GetComponent<Hero>();
        heroData = hero.CurrentData;

        MaxHp = heroData.hp;
        CurrentHp = heroData.hp;
        MaxMp = 0f;
        CurrentMp = 0f;
        CurrentAtk = heroData.atk;
        CurrentDef = heroData.def;
        CriticalRate = 0f;
        EvasionRate = 0f;
    }
}
