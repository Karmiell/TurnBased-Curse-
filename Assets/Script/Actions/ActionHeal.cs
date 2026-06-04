using UnityEngine;
using System;

public class ActionHeal : BaseAction, ActionInterface
{
    public event EventHandler<ActionInterface.OnStateChangeEventArgs> OnStateChange;

    public override void ActionInteract()
    {
       OnStateChange?.Invoke(this, new ActionInterface.OnStateChangeEventArgs(){ actionType = BaseAction.State.Healing});
    }
}
