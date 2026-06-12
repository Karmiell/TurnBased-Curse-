using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class visualGrid : MonoBehaviour
{
    [SerializeField]private Transform visualObject;

    private VisualGridSingle[,] visualGridSingleArray;
    private int altura;
    private int largura;


    private void Awake()
    {
        altura = LevelGrid.Instance.meuGrid.GetAltura();
        largura = LevelGrid.Instance.meuGrid.GetLargura();
    }
    private void Start()
    {
        HandlerSelection.Instance.OnActionSelectChangeVisual += HandlerSelection_OnActionSelectChange;
        CreateAllVisualObjects(visualObject);
        HideAllObject(visualGridSingleArray);
    }


    private void HandlerSelection_OnActionSelectChange ( object sendler, EventArgs e)
    {
        UpdateVisualObject(visualGridSingleArray);
    }

    private void UpdateVisualObject(VisualGridSingle[,] visualGridSingles)
    {
        HideAllObject(visualGridSingles);
        ShowAllObjectAtListAtualAction(visualGridSingles, HandlerSelection.Instance.GetSelectAction().GetGridPositionList());
    }

    public void ShowAllObjectAtListAtualAction(VisualGridSingle[,] visualGridSingles, List<GridPosition> gridPositions = default)
    {
        for (int j = 0; j < gridPositions.Count; j++)
        {
        visualGridSingles[gridPositions[j].X,gridPositions[j].Z].Show();
        }
        
    }
    public void HideAllObject(VisualGridSingle[,] visualGridSingles)
    {
        foreach(var atual in visualGridSingles)
        {
            atual.Hide();
        }
    }

    public void CreateAllVisualObjects(Transform visualObject)
    {
        visualGridSingleArray = new VisualGridSingle[altura,largura];

        for (int i = 0; i < altura; i++)
        {
            for (int j = 0; j < largura; j++)
            {
                var wordPosition = LevelGrid.Instance.meuGrid.GetWorldPosition(i,j);

                var visual = Instantiate(visualObject, wordPosition, quaternion.identity);

                visualGridSingleArray[i,j] = visual.GetComponent<VisualGridSingle>();
            }
        }
    }
}
