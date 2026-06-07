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


    private void Update()
    {
        if(!actionStart)return;
        if(!Mouse.current.leftButton.wasPressedThisFrame)return;
        var position = LevelGrid.Instance.meuGrid.GetWorldPosition(gridPosition.X,gridPosition.Z);
        tempObject = Instantiate(visualHealing,position,Quaternion.identity);
        Invoke("Destroy", 2f);
        
    }

    private void Destroy()
    {
        Destroy(tempObject);
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
        int maxRange = 2;
        var tempList = new List<GridPosition>();
        for (int i = -maxRange ; i < maxRange ; i++)
        {
            for (int j = -maxRange; j < maxRange ; j++)
            {
                var testPosition = new GridPosition(i,j) + gridPosition;
                if(!LevelGrid.Instance.meuGrid.ValidGridPosition(testPosition))continue;
                if (!LevelGrid.Instance.meuGrid.GetGridObject(testPosition).HasNotUnit())
                {
                    tempList.Add(testPosition);
                } 
            }
        }
        validGridPositonList = tempList;
    }
    public override void ClearList()
    {
        validGridPositonList = new List<GridPosition>();
    }
    public override List<GridPosition> GetGridPositionList() {
        return validGridPositonList = new List<GridPosition>();
    }
    public override string GetNameAction() => "Heal";
}
