using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
[SerializeField]private float VDM = 5f;
[SerializeField]public LayerMask layerMaskGround;
[SerializeField]private List<ActionSO> actionSOList;
private Vector3 destination;
private Vector3 moveDir;

public event Action<Vector3> OnWalkingValue;
public event Action<List<ActionSO>> OnActionsAvalible;
public event EventHandler OnSelectTrue;
private float stopDistance = .1f;
private GridPosition gridPosition;
private GridPosition newPosition;

    private void Awake()
    {
        destination = transform.position;
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
    moveDir = (destination - transform.position).normalized;
    newPosition = LevelGrid.Instance.meuGrid.GetGridPosition(transform.position);
    
    GridMoviment();
    Movement();

    OnWalkingValue?.Invoke(moveDir);      
    }

    public void Move(Vector3 destination)
    {
    this.destination = destination;
    }

    public void GridMoviment()
    {
        if(gridPosition != newPosition)
        {
        LevelGrid.Instance.ChangeGridPosition(gridPosition, newPosition, this);
        gridPosition = newPosition;
        }
    }

    public void Movement()
    {        
    if (Vector3.Distance(destination, transform.position) > stopDistance)transform.position += moveDir * Time.deltaTime * VDM; 
    else moveDir = Vector3.zero;
    }

    public void ShowActionAvalible()
    {
        OnActionsAvalible?.Invoke(actionSOList);
    }
}
