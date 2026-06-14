using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

[SerializeField]public LayerMask layerMaskGround;
[SerializeField]private bool isEnemy;
[SerializeField]private int maxPointsMove = 6;
[SerializeField]private int maxPointsActions = 5;


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
       ResetPoints();
        actionMove = GetComponent<ActionMove>();
        ActionsAvalibleArray = GetComponents<BaseAction>();
    }

    void Start()
    {
        HandlerSelection.Instance.OnAtualSelect += ChangeVisual;
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;

        gridPosition = LevelGrid.Instance.meuGrid.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(this, gridPosition);
      
    }
private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        if(isEnemy && TurnSystem.Instance.GetUnitTurnBool())return;
        if(!isEnemy && !TurnSystem.Instance.GetUnitTurnBool())return;
        ResetPoints();
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
    public bool TryDoAction(BaseAction baseAction, GridPosition gridPosition)
    {
     if(baseAction is ActionMove)
        {
            if(baseAction.SetCostAtGridPosition(gridPosition, GetGridPosition()) <= movePoins)
            {
            return true;
            }
        }
        else
        {
            if(baseAction.ActionCost() <= actionsPoint)
            {
               
                return true;
            }
        }
        return false;
    }
    public void SubSetMovePoint(int cost)
    {
        movePoins -= cost;
        OnPointsChange?.Invoke(this, EventArgs.Empty);
    }
    public void SubSetActionPoint(int cost)
    {
        actionsPoint -= cost;
        OnPointsChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetMovePoints() => movePoins;
    private void ResetPoints()
    {
    actionsPoint = maxPointsActions;
    movePoins = maxPointsMove;
    }

    public bool HasEnemy() => isEnemy;
}
