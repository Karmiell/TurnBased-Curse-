using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

[SerializeField]public LayerMask layerMaskGround;


public event EventHandler OnSelectTrue;


private BaseAction[] ActionsAvalibleArray;
private ActionMove actionMove;
private GridPosition gridPosition;
private GridPosition newPosition;



    private void Awake()
    {
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
}
