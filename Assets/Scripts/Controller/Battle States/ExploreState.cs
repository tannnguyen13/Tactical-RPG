using System.Collections;
using UnityEngine;

public class ExploreState : BattleState
{
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
        Debug.Log(e.info + pos);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
            owner.ChangeState<CommandSelectionState>();
            
    }
}
