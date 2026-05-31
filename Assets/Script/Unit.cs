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

    private void Awake()
    {
        destination = transform.position;
    }
    void Start()
    {
        HandlerSelection.Instance.OnAtualSelect += ChangeVisual;
        gridPosition = LevelGrid.meuGrid.GetGridPosition(transform.position);
        gridPosition.AddUnitAtGridPosition(this, gridPosition);
    }

    private void ChangeVisual(bool SelectVisual)
    {
        if(SelectVisual)OnSelectTrue?.Invoke(this,EventArgs.Empty);
    }

    void Update()
    {
    var moveDir = (destination - transform.position).normalized;

    if (Vector3.Distance(destination, transform.position) > stopDistance){
        transform.position += moveDir * Time.deltaTime * VDM;
        gridPosition.ChangeGridPosition(gridPosition, LevelGrid.meuGrid.GetGridPosition(transform.position), this);
    }
    else {

        moveDir = Vector3.zero;
    }
    OnWalkingValue?.Invoke(moveDir);
            
    }

    public void Move(Vector3 destination)
    {
    this.destination = destination;

    }

}
