using UnityEngine;
using System;

public interface ActionInterface
{
     public event EventHandler<OnStateChangeEventArgs> OnStateChange;
      public class OnStateChangeEventArgs : EventArgs
    {
        public BaseAction.State actionType;
    }
}
