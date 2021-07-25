using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STypeHitRate : HitRate
{
    public override int Calculate (Tile target)
    {
        Unit defender = target.content.GetComponent<Unit>();
        if(AutomaticMiss(defender))
            return Final(100);

        if (AutomaticHit(defender))
            return Final(0);

        int con = GetConstitution(defender);
        con = AdjustForStatusEffects(defender, con);
        con = AdjustForStatusEffects(defender, con);
        con = Mathf.Clamp(con, 0, 100);
        return Final(con);
    }

    int GetConstitution(Unit target)
    {
        Stats s = target.GetComponentInParent<Stats>();
        return s[StatTypes.CON];
    }

    int AdjustForRelativeFacing (Unit target, int rate)
    {
        switch(attacker.GetFacing(target))
        {
            case Facings.Front:
                return rate;
            case Facings.Side:
                return rate - 10;
            default:
                return rate - 20;
        }
    }
}
