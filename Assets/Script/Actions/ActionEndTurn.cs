using UnityEngine;
using System;

public class ActionEndTurn : BaseAction, ActionInterface
{
    public event EventHandler<ActionInterface.OnStateChangeEventArgs> OnStateChange;

    public override void ActionInteract()
    {
       OnStateChange?.Invoke(this, new ActionInterface.OnStateChangeEventArgs(){ actionType = BaseAction.State.EndTurn});
    }
}
