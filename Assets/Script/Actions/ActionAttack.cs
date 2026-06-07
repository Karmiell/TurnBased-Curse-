using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ActionAttack : BaseAction
{
    private GridPosition gridPosition;

    void Update()
    {
        if (actionStart)
        {
            if(AttackAction())return;           
    
        }
    }
    private bool actionStart;
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
        AttackAction();
    }
    private bool AttackAction()
    {
        if(Keyboard.current.fKey.wasPressedThisFrame)
        {
            actionStart = false;
            OnActionComplete();
            return true;
        }
        return false;
    }

    public override bool ValidGridPosition(GridPosition gridPosition)
    {
      return validGridPositonList.Contains(gridPosition);
    }
    public override void SetValidGridPositionList(GridPosition gridPosition)
    {
        int maxRange = 4;
        int minRange = 2;
        var tempListNoN = new List<GridPosition>();
        var tempList = new List<GridPosition>();
        for (int i = -minRange ; i <= minRange ; i++)
        {
            for (int j = -minRange; j <= minRange ; j++)
            {
                var newGridPosition =  new GridPosition(i,j);
                var testPosition = newGridPosition + gridPosition;
                tempListNoN.Add(testPosition);

            }
        }
        for (int i = -maxRange ; i <= maxRange ; i++)
        {
            for (int j = -maxRange; j <= maxRange ; j++)
            {
                var newGridPosition =  new GridPosition(i,j);
                var testPosition = newGridPosition + gridPosition;

                if(!LevelGrid.Instance.meuGrid.ValidGridPosition(testPosition))continue;
                if(tempListNoN.Contains(testPosition))continue;
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
    public override string GetNameAction() => "Attack";
}
