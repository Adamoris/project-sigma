class DefenceBuff : Buff
{
    private int _factor;

    public DefenceBuff(int duration, int factor)
    {
        Duration = duration;
        _factor = factor;
    }

    public int Duration { get; set; }
    public void Apply(Unit unit)
    {
        unit.Def += _factor;
    }

    public void Undo(Unit unit)
    {
        unit.Def -= _factor;
    }

    public Buff Clone()
    {
        return new DefenceBuff(Duration, _factor);
    }
}

