using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
[SerializeField]private float VDM = 5f;
[SerializeField]public LayerMask layerMaskGround;
private Vector3 destination;

public event Action<Vector3> OnWalkingValue;
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
    var moveDir = (destination - transform.position).normalized;
    newPosition = LevelGrid.Instance.meuGrid.GetGridPosition(transform.position);
    
    if(gridPosition != newPosition)
        {
            LevelGrid.Instance.ChangeGridPosition(gridPosition, newPosition, this);
            gridPosition = newPosition;
        }
    

    if (Vector3.Distance(destination, transform.position) > stopDistance)
    {
        
        transform.position += moveDir * Time.deltaTime * VDM;
        
        
    }
    else
    {
        
        moveDir = Vector3.zero;
    }
    OnWalkingValue?.Invoke(moveDir);
            
    }

    public void Move(Vector3 destination)
    {
    this.destination = destination;

    }

}
