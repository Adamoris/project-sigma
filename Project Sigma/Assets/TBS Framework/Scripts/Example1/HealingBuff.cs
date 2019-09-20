using UnityEngine;

class HealingBuff : Buff
{
    private int _healingFactor;

    public HealingBuff(int duration, int healingFactor)
    {
        _healingFactor = healingFactor;
        Duration = duration;
    }

    public int Duration { get; set; }
    public void Apply(Unit unit)
    {
        AddHP(unit, _healingFactor);
    }
    public void Undo(Unit unit)
    {
        //Note that healing buff has empty Undo method implementation.
    }

    public Buff Clone()
    {
        return new HealingBuff(Duration, _healingFactor);
    }

    private void AddHP(Unit unit, int amount)
    {
        unit.HP = Mathf.Clamp(unit.HP + amount, 0, unit.TotalHP);
    }
}

