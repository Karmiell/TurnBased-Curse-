using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class HandlerSelection : MonoBehaviour
{

public static HandlerSelection Instance{get; private set;}
 private Unit unitSelect;
 private BaseAction actionSelect;
private bool isBusy;

 public event Action<bool> OnAtualSelect;
 public event EventHandler OnActionSelectChangeVisual;


 [SerializeField]private LayerMask layerUnit;

    private void Awake()
    {
         if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        unitSelect = FindAnyObjectByType<Unit>();
        OnAtualSelect?.Invoke(true);
        actionSelect = unitSelect.GetActionMove();
    }

    private void Update()
    {
    if(isBusy)return;

     if(Mouse.current.leftButton.wasPressedThisFrame)
    {
    if(!unitSelect.IsUnityNull()){
        actionSelect = unitSelect.GetActionMove();
        OnActionSelectChangeVisual?.Invoke(this, EventArgs.Empty);
        SetBusy();
    }
    if(TryHandleSelection()){
        OnAtualSelect?.Invoke(TryHandleSelection());
        ClearBusy();
        return;
        }
    
    if(!unitSelect)return;

    actionSelect.SetValidGridPositionList(unitSelect.GetGridPosition());
    var position = LevelGrid.Instance.meuGrid.GetGridPosition(MouseWorld.Instance.GetWorldMousePosition().point);

    if(actionSelect.ValidGridPosition(position))
    {
    actionSelect.ActionInteract(position, ClearBusy); 
    }
    ClearBusy();
    } 
        
    }

    private bool TryHandleSelection()
    {
    var tryUnit = MouseWorld.Instance.GetWorldMousePosition(layerUnit).transform;
    if (!tryUnit.IsUnityNull())
    {
    if(tryUnit.TryGetComponent<Unit>(out unitSelect)){
        return true;
    }
    else {
        return false;
    }
        
    } return false;

    }

    public Unit GetSelectUnit() => unitSelect;
    public BaseAction GetSelectAction() => actionSelect;

    private void SetBusy()  {
        OnActionSelectChangeVisual?.Invoke(this, EventArgs.Empty);
        isBusy = true;
}    private void ClearBusy() => isBusy = false;
}