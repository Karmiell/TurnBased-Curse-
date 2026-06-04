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

    public void ChangeGridPosition(GridPosition oldPosition, GridPosition newPosition, Unit unit)
    {
        RemoveUnitAtGridPosition(unit, oldPosition);
        AddUnitAtGridPosition(unit, newPosition);
    }
}
  
