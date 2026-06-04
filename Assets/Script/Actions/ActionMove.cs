using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionMove : BaseAction, ActionInterface
{
    public event EventHandler<ActionInterface.OnStateChangeEventArgs> OnStateChange;

    public override void ActionInteract()
    {
       OnStateChange?.Invoke(this, new ActionInterface.OnStateChangeEventArgs(){ actionType = BaseAction.State.Move});
    }
}
