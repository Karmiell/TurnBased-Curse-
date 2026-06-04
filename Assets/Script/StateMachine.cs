using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour, ActionInterface
{
    private BaseAction.State actionType;
    public event EventHandler<ActionInterface.OnStateChangeEventArgs> OnStateChange;

    private void OnEnable() => OnStateChange += ChangeStateAtual;


    private void OnDisable()=> OnStateChange += ChangeStateAtual;

    public void ChangeStateAtual(object sendler, ActionInterface.OnStateChangeEventArgs e)
    {
        actionType = e.actionType;
    }
    

    private void Update()
    {
        switch (actionType)
        {
            case BaseAction.State.UnitTurn :
            UnitTurnHandler();
            break;

            case BaseAction.State.EnemyTurn :
            EnemyActionsHandler();
            break;

            case BaseAction.State.Attacking :
            ActionAttackHandler();
            break;

            case BaseAction.State.Move : 
            ActionMoveHandler();
            break;

            case BaseAction.State.Healing :
            ActionHealHandler();
            break;

            case BaseAction.State.EndTurn :
            actionType = BaseAction.State.EnemyTurn;
            break;

            default :
            break;
        }
    }

    public void UnitTurnHandler()
    {
        if(HandlerSelection.Instance.GetSelectUnit().IsUnityNull())return;
        HandlerSelection.Instance.GetSelectUnit().ShowActionAvalible();
        
    }
    public void EnemyActionsHandler()
    {
        
    }
    public void ActionAttackHandler()
    {
        
    }
    public void ActionMoveHandler()
    {
        
    }
    public void ActionHealHandler()
    {
        
    }
}
