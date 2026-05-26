using Unity.VisualScripting;
using UnityEngine;
using System;

public class HandlerSelection : MonoBehaviour
{

public static HandlerSelection Instance{get; private set;}
 private Unit unitSelect;
 public event Action<bool> OnAtualSelect;

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

    private void Update()
    {
     if(Input.GetMouseButtonDown(0))
    {
    
    if(TryHandleSelection()){
        OnAtualSelect?.Invoke(TryHandleSelection());
        return;
        }
    if(!unitSelect)return;
    unitSelect.Move(MouseWorld.Instance.GetWorldMousePosition(unitSelect.layerMaskGround).point);    
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
    public Unit GetSelectUnit()
    {
        return unitSelect;
    }

    
}
