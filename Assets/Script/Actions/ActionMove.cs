using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class ActionMove : BaseAction
{
    [SerializeField]private float VDM = 5f;


    public event Action<Vector3> OnWalkingValue;


    private Vector3 moveDir;
    private Vector3 destination;
    private bool startAction = false;
    private float stopDistance = .1f;

    private Unit unit;
    


   private void Awake()
    {
        unit = GetComponent<Unit>();
        destination = transform.position;
        moveDir = Vector3.zero;
        ClearList();
    }

    private void Update()
    {
    if(!startAction)return;
    Movement();
        
    OnWalkingValue?.Invoke(moveDir);  
    }

    public override void ActionInteract(GridPosition gridPosition, Action action)
    {
        OnActionComplete = action;
    if(!ValidGridPosition(gridPosition)){
        OnActionComplete();
        return;
    }
    this.destination = LevelGrid.Instance.meuGrid.GetWorldPosition(gridPosition.X,gridPosition.Z);
    
    startAction = true;

    }
    public void Movement()
    {
    moveDir = (destination - transform.position).normalized;        
    if (Vector3.Distance(destination, transform.position) > stopDistance)transform.position += moveDir * Time.deltaTime * VDM; 
    else
    {
        ClearList();
        moveDir = Vector3.zero;
        startAction = false;

        OnActionComplete();
    }
    }
    
    public override bool ValidGridPosition(GridPosition gridPosition)
    {
        return validGridPositonList.Contains(gridPosition);
    }

    public override void SetValidGridPositionList(GridPosition atualGridPosition)
    {
        int maxMoveDistance = 2;
        List<GridPosition> gridPositionAtualList = new List<GridPosition>();
        for (int i = -maxMoveDistance; i <= maxMoveDistance; i++)
        {
            for (int j = -maxMoveDistance ; j <= maxMoveDistance; j++)
            {
                var gridPosition = new GridPosition(i,j);
                var testValidPosition = gridPosition + atualGridPosition;
                if(testValidPosition == atualGridPosition || 
                !LevelGrid.Instance.meuGrid.ValidGridPositionAll(testValidPosition))continue;
                gridPositionAtualList.Add(testValidPosition);
            }
        }

    validGridPositonList = gridPositionAtualList;
    }
     public override void ClearList()
    {
        validGridPositonList = new List<GridPosition>();
    }
    public override List<GridPosition> GetGridPositionList() => validGridPositonList;
    public override string GetNameAction() => "Move";
     
}
