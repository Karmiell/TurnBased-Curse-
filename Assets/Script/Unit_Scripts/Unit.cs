using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

[SerializeField]public LayerMask layerMaskGround;


public event EventHandler OnSelectTrue;
public event EventHandler OnPointsChange;


private BaseAction[] ActionsAvalibleArray;
private ActionMove actionMove;
private GridPosition gridPosition;
private GridPosition newPosition;
private int actionsPoint;
private int movePoins;





    private void Awake()
    {
        actionsPoint = 5;
        movePoins = 6;
        actionMove = GetComponent<ActionMove>();
        ActionsAvalibleArray = GetComponents<BaseAction>();
    }

    void Start()
    {
        HandlerSelection.Instance.OnAtualSelect += ChangeVisual;
        gridPosition = LevelGrid.Instance.meuGrid.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(this, gridPosition);
        
        
    }

    private void ChangeVisual(bool SelectVisual)
    {
        if(SelectVisual)OnSelectTrue?.Invoke(this,EventArgs.Empty);
    }

    void Update()
    {
    newPosition = LevelGrid.Instance.meuGrid.GetGridPosition(transform.position);
    
    GridMoviment();   
    }

    public ActionMove GetActionMove() => actionMove;
    public void GridMoviment()
    {
        if(gridPosition != newPosition)
        {
        LevelGrid.Instance.ChangeGridPosition(gridPosition, newPosition, this);
        gridPosition = newPosition;
        }
    }

    public BaseAction[] GetBaseActionsArray() => ActionsAvalibleArray;
 
    public GridPosition GetGridPosition() => gridPosition;
    public int GetActionPoint() => actionsPoint;
    public bool TryDoAction(BaseAction baseAction)
    {
     if(baseAction is ActionMove)
        {
            if(baseAction.ActionCost() < movePoins)
            {
            SubSetMovePoint(baseAction.ActionCost());
            return true;
            }
        }
        else
        {
            if(baseAction.ActionCost() < actionsPoint)
            {
                SubSetActionPoint(baseAction.ActionCost());
                return true;
            }
        }
        return false;
    }
    private void SubSetMovePoint(int cost)
    {
        movePoins -= cost;
        OnPointsChange?.Invoke(this, EventArgs.Empty);
    }
    private void SubSetActionPoint(int cost)
    {
        actionsPoint -= cost;
        OnPointsChange?.Invoke(this, EventArgs.Empty);
    }
}
