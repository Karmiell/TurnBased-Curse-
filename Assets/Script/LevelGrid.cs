using UnityEditor.Rendering;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    static public GridSystem meuGrid;
    [SerializeField]private Transform textGridPrefab;
    
    
    void Awake()
    {
        meuGrid = new GridSystem(9,9);
        meuGrid.GridTextAtGridSystem(textGridPrefab);
    }
    private void Update()
    {
      Debug.Log(meuGrid.GetGridPosition(MouseWorld.Instance.GetWorldMousePosition().point).GetUnitListCount());

    }
}
  
