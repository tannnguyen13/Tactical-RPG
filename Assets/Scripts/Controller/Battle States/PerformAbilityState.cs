using System.Collections;
using UnityEngine;

public class PerformAbilityState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
            turn.lockMove = true;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        //TODO play animations, etc
        yield return null;
        //TODO apply attack ability effect, etc
        ApplyAbility();

        if (IsBattleOver())
            owner.ChangeState<CutSceneState>();
        else if (!UnitHasControl())
            owner.ChangeState<SelectUnitState>();
        else if (turn.hasUnitMoved)
            owner.ChangeState<EndFacingState>();
        else
            owner.ChangeState<CommandSelectionState>();
    }

    bool UnitHasControl()
    {
        return turn.actor.GetComponentInChildren<KnockOutStatusEffect>() == null;
    }

    void ApplyAbility()
    {
        BaseAbilityEffect[] effects = turn.ability.GetComponentsInChildren<BaseAbilityEffect>();
        for(int i = 0; i < turn.targets.Count; ++i)
        {
            Tile target = turn.targets[i];
            for(int j = 0; j < effects.Length; ++j)
            {
                BaseAbilityEffect effect = effects[j];
                AbilityEffectTarget targeter = effect.GetComponent<AbilityEffectTarget>();
                if(targeter.isTarget(target))
                {
                    HitRate rate = effect.GetComponent<HitRate>();
                    int chance = rate.Calculate(target);
                    if(UnityEngine.Random.Range(0, 101) > chance)
                    {
                        continue;
                    }
                    effect.Apply(target);
                }
            }
        }
    }
}
