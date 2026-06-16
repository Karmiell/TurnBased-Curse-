using UnityEditor.Rendering;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance{get; private set;}

    public GridSystem meuGrid;
    [SerializeField]private Transform textGridPrefab;
    
    
    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
            return;
            
        }
        Instance = this;
        meuGrid = new GridSystem(9,9);
        meuGrid.GridTextAtGridSystem(textGridPrefab);
    }

    
     public void AddUnitAtGridPosition(Unit unit, GridPosition gridPosition)
    {
        meuGrid.GetGridObject(gridPosition).GetUnitsList().Add(unit);
    }
    public void RemoveUnitAtGridPosition(Unit unit, GridPosition gridPosition)
    {
        meuGrid.GetGridObject(gridPosition).GetUnitsList().Remove(unit);
    }

    public void ChangeGridPositionUnit(GridPosition oldPosition, GridPosition newPosition, Unit unit)
    {
        RemoveUnitAtGridPosition(unit, oldPosition);
        AddUnitAtGridPosition(unit, newPosition);
    }
    public bool HasNotUnitAtGridPosition(GridPosition gridPosition) => meuGrid.GetGridObject(gridPosition).HasNotUnit();
    public void AddDamagebleAtGridPosition(IDamageble damageble, GridPosition gridPosition)
    {
        meuGrid.GetGridObject(gridPosition).GetDamageblesList().Add(damageble);
    }
     public void RemoveDamagebleAtGridPosition(IDamageble damageble, GridPosition gridPosition)
    {
        meuGrid.GetGridObject(gridPosition).GetDamageblesList().Remove(damageble);
    }
    public void ChangeGridPositionDamageble(GridPosition oldPosition, GridPosition newPosition, IDamageble damageble)
    {
        RemoveDamagebleAtGridPosition(damageble, oldPosition);
        AddDamagebleAtGridPosition(damageble, newPosition);
    }

}
  
