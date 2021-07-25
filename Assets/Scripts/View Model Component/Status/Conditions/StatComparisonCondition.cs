using System.Collections;
using System;
using UnityEngine;

public class StatComparisonCondition : StatusCondition
{
    public StatTypes type { get; private set; }
    public int value { get; private set; }
    public Func<bool> condition { get; private set; }
    Stats stats;

    private void Awake()
    {
        stats = GetComponentInParent<Stats>();
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnStatChanged, Stats.DidChangeNotification(type), stats);
    }

    public void Init(StatTypes type, int value, Func<bool> condition)
    {
        this.type = type;
        this.value = value;
        this.condition = condition;
        this.AddObserver(OnStatChanged, Stats.DidChangeNotification(type), stats);
    }

    public bool EqualTo()
    {
        return stats[type] == value;
    }

    public bool LessThan()
    {
        return stats[type] < value;
    }

    public bool LessThanOrEqualTo ()
    {
        return stats[type] > value;
    }

    public bool GreaterThan ()
    {
        return stats[type] > value;
    }

    public bool GreaterThanOrEqualTo()
    {
        return stats[type] >= value;
    }

    void OnStatChanged (object sender, object args)
    {
        if (condition != null && !condition())
            Remove();
    }
}
