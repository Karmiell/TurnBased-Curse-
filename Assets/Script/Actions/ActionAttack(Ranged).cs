using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ActionAttack : BaseAction
{
    private GridPosition gridPosition;

    void Update()
    {
        if (actionStart)
        {
            AttackAction();        
    
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
    }
    private void AttackAction()
    {
       
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(EventSystem.current.IsPointerOverGameObject())return;
            var position = LevelGrid.Instance.meuGrid.GetGridPosition(MouseWorld.Instance.GetWorldMousePosition().point);
            if(!ValidGridPosition(position))return;

            actionStart = false;
            var gridObject = LevelGrid.Instance.meuGrid.GetGridObject(position);
            foreach(var atual in gridObject.GetDamageblesList())
            {
                atual.TakeDamage(10);
            }

            HandlerSelection.Instance.GetSelectUnit().SubSetActionPoint(ActionCost());
            OnActionComplete();
        }
       
    }
    private int DamageAmount()
    {
        return HandlerSelection.Instance.GetSelectUnit().GetStatusBase().statusBase.forca;
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
                if(math.abs(i + j) > minRange || -(i - j) < -minRange || (i - j) < -minRange)continue;
                var newGridPosition =  new GridPosition(i,j);
                var testPosition = newGridPosition + gridPosition;
                tempListNoN.Add(testPosition);

            }
        }
        for (int i = -maxRange ; i <= maxRange ; i++)
        {
            for (int j = -maxRange; j <= maxRange ; j++)
            {
                if(math.abs(i + j) > maxRange || -(i - j) < -maxRange || (i - j) < -maxRange)continue;
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
