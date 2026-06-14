using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ActionHeal : BaseAction
{
    [SerializeField]private GameObject visualHealing;

    private bool actionStart = false;
    private GridPosition gridPosition;

    private void Update()
    {
        HealAction();
    }

    private void HealAction()
    {
        if(actionStart)
        {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
        if(EventSystem.current.IsPointerOverGameObject())return;    
        var position = LevelGrid.Instance.meuGrid.GetGridPosition(MouseWorld.Instance.GetWorldMousePosition().point);
        if(!ValidGridPosition(position))return;

        actionStart = false; 
        Instantiate(visualHealing,LevelGrid.Instance.meuGrid.GetWorldPosition(position.X,position.Z),Quaternion.identity);
        EndAction();
        }
        }
    }

    private void EndAction()
    {
        HandlerSelection.Instance.GetSelectUnit().SubSetActionPoint(ActionCost());
        ClearList();
        OnActionComplete();
    }
    public override void ActionInteract( GridPosition gridPosition, Action action)
    {
        OnActionComplete = action;

        if(!ValidGridPosition(gridPosition))
        {
            OnActionComplete();
            return;
        }
        
        this.gridPosition = gridPosition;
        actionStart = true;
        
    }

    public override bool ValidGridPosition(GridPosition gridPosition)
    {
      return validGridPositonList.Contains(gridPosition);
    }
    public override void SetValidGridPositionList(GridPosition gridPosition)
    {
        int maxRange = 5;
        var tempList = new List<GridPosition>();
        for (int i = -maxRange ; i <= maxRange ; i++)
        {
            for (int j = -maxRange; j <= maxRange ; j++)
            {
                var newGridPosition =  new GridPosition(i,j);
                var testPosition = newGridPosition + gridPosition;

                if(!LevelGrid.Instance.meuGrid.ValidGridPosition(testPosition))continue;
                if (LevelGrid.Instance.HasNotUnitAtGridPosition(testPosition))continue;
                
                tempList.Add(testPosition);   
            }
        }
        validGridPositonList = tempList;
    }
    public override void ClearList()
    {
        validGridPositonList = new List<GridPosition>();
    }
    public override List<GridPosition> GetGridPositionList() {
        return validGridPositonList;
    }
    public override string GetNameAction() => "Heal";
    public override int ActionCost() => 2;
    
}
