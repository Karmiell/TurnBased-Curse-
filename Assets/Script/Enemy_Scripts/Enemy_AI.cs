using System;
using System.Threading;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
private enum TurnStates
    {
        WaitingUnitTurn, 
        TakingTurn,
        Busy
    }

    private TurnStates curentState;
    float atualtimer = 0f;

    private void Awake()
    {
        curentState = TurnStates.WaitingUnitTurn;
    }
    private void Start()
    {
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange; 
    }

    private void Update()
    {
        if(TurnSystem.Instance.GetUnitTurnBool())return;

        switch (curentState)
        {
            case TurnStates.WaitingUnitTurn:
            Debug.Log($"Inimigo {gameObject.name} Esperando...");
            break;

            case TurnStates.TakingTurn:
            if(Timer(2))return;
           
            foreach(var atual in UnitManager.Instance.GetEnemyList())
            {
            BaseAction actionMove = atual.GetActionMove();
            actionMove.SetValidGridPositionList(atual.GetGridPosition());
            GridPosition gridPosition = actionMove.GetGridPositionList()[UnityEngine.Random.Range(0, actionMove.GetGridPositionList().Count)];
            if(!atual.TryDoAction(actionMove, gridPosition))continue;
            curentState = TurnStates.Busy;
             HandleSelection(atual,gridPosition);
             
            }
            
            break;

            case TurnStates.Busy:
            Debug.Log($"Inimigo {gameObject.name} Ocupado");
            break;
        }
    }

private void HandleSelection(Unit unit, GridPosition gridPosition)
    {

        HandlerSelection.Instance.SetUnitEnemySelect(unit);
        BaseAction actionMove = unit.GetActionMove();
        

        actionMove.ActionInteract(gridPosition, ClearActionState);
          
            
    }
private void ClearActionState()
    {
     curentState = TurnStates.TakingTurn;    
    }
    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        if(TurnSystem.Instance.GetUnitTurnBool())return;
        curentState = TurnStates.TakingTurn;
    }

    private bool TryDoBestAction(Unit unit)
    {
        var actionMove = unit.GetActionMove();
       


        return false;
    }
    private void Clear()
    {
        
    }
    private bool Timer(float time)
    {
        
        atualtimer += Time.deltaTime;
        if(atualtimer < time)return true;
        else
        {
        atualtimer = 0f;
        return false;
        }
    }
}
