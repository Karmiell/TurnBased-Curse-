using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextGridVisual : MonoBehaviour
{
 private TextMeshPro gridText;
 private GridObject gridObject;
 private GridPosition gridPosition;

    private void Start()
    {
        gridText = GetComponentInChildren<TextMeshPro>();
        if(!gridText){
            Debug.Log($"Não foi possivel instanciar o componenete {gridText} no {gameObject.name}");
        }
        gridPosition = LevelGrid.Instance.meuGrid.GetGridPosition(transform.position);
        gridObject = LevelGrid.Instance.meuGrid.GetGridObject(gridPosition);

        if(!gridText || gridObject.IsUnityNull())return;
        ChangeTextVisual(gridText);
    }
    private void Update()
    {
        ChangeTextVisual(gridText);
    }

    private void ChangeTextVisual(TextMeshPro textMeshPro)
    {
        if(textMeshPro. text == gridObject.ToString())return;
         textMeshPro.text = gridObject.ToString();
    }
}
