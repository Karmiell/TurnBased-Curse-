using UnityEngine;

public class TestGridInicialitor : MonoBehaviour
{
    private GridSystem meuGrid;
    
    void Start()
    {
        meuGrid = new GridSystem(10,10);
    }
}
  
