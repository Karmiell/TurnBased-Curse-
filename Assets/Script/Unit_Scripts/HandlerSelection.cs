using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class HandlerSelection : MonoBehaviour
{
 [SerializeField]private LayerMask layerUnit;


public static HandlerSelection Instance{get; private set;}


 private Unit unitSelect;
 private BaseAction actionSelect;
private bool isBusy;


 public event Action<bool> OnAtualSelect;
 public event EventHandler OnActionSelectChangeVisual;

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


    private void Update()
    {
    if(isBusy)return;

    if(TryHandleSelection())return;
    
    HandleActionSelect();
    }
        
    

    public void HandleActionSelect()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(EventSystem.current.IsPointerOverGameObject())return;
            var position = LevelGrid.Instance.meuGrid.GetGridPosition(MouseWorld.Instance.GetWorldMousePosition().point);
            if(actionSelect.IsUnityNull())return;
            if(TurnSelection(unitSelect))return;
            if(!unitSelect.TryDoAction(actionSelect, position))return;

            SetBusy();
            actionSelect.ActionInteract(position,ClearBusy);

            
        }
    }

    private bool TryHandleSelection()
    {
    if(Mouse.current.leftButton.wasPressedThisFrame){
    if(EventSystem.current.IsPointerOverGameObject())return false;
    var tryUnit = MouseWorld.Instance.GetWorldMousePosition(layerUnit).transform;
    if (tryUnit.IsUnityNull())return false;
    
    if(tryUnit.TryGetComponent<Unit>(out var result)){
        if(result == unitSelect)return false;
        if(result.HasEnemy())return false;
        SetUnitSelect(result);
        SetActionSelect(unitSelect.GetActionMove());
        return true;
    }
    
    }
    return false;

    }

    public Unit GetSelectUnit() => unitSelect;
    public BaseAction GetSelectAction() => actionSelect;

    private void SetBusy() 
    {
        isBusy = true;
    }  
        private void ClearBusy() 
    {
        isBusy = false;
        OnActionSelectChangeVisual?.Invoke(this,EventArgs.Empty);
    }
    public void SetUnitSelect(Unit unit)
    {
        unitSelect = unit;
        OnAtualSelect?.Invoke(true);
    }

    public void SetActionSelect(BaseAction baseAction)
    {
        //if(baseAction == actionSelect)return;
        actionSelect = baseAction;
        actionSelect.SetValidGridPositionList(unitSelect.GetGridPosition());
        OnActionSelectChangeVisual?.Invoke(this,EventArgs.Empty);
    }

    private bool TurnSelection(Unit unit)
    {
        if(unit.HasEnemy() && TurnSystem.Instance.GetUnitTurnBool())return true;
        if(!unit.HasEnemy() && !TurnSystem.Instance.GetUnitTurnBool())return true;

        return false;
    }

}