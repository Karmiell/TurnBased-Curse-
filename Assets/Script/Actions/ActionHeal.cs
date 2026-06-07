using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public class ActionHeal : BaseAction
{
    [SerializeField]private GameObject visualHealing;

    private bool actionStart = false;
    private GameObject tempObject;
    private GridPosition gridPosition;


    private void HealAction()
    {
        if(actionStart)
        {
        actionStart = false;
            
        var position = LevelGrid.Instance.meuGrid.GetWorldPosition(gridPosition.X,gridPosition.Z);
        tempObject = Instantiate(visualHealing,position,Quaternion.identity);
        EndAction();
        }
    }

    private void EndAction()
    {
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
        HealAction();
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
}
