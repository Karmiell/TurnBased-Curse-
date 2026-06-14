using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Action OnActionComplete;
  
    protected List<GridPosition> validGridPositonList;
    public virtual void ActionInteract( GridPosition gridPosition, Action action){}

    public virtual bool ValidGridPosition(GridPosition gridPosition) => true;
    public abstract void SetValidGridPositionList(GridPosition gridPosition);
    public abstract void ClearList();
    public virtual List<GridPosition> GetGridPositionList() => validGridPositonList = new List<GridPosition>();
    public abstract string GetNameAction();
    public virtual int ActionCost() => 1;
    
    public virtual int SetCostAtGridPosition(GridPosition destino, GridPosition origem) => 1;
  
   

    





}
