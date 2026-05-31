using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextGridVisual : MonoBehaviour
{
 private TextMeshPro gridText;
 private GridPosition gridPosition;

    private void Start()
    {
        gridText = GetComponentInChildren<TextMeshPro>();
        if(!gridText){
            Debug.Log($"Não foi possivel instanciar o componenete {gridText} no {gameObject.name}");
        }
        gridPosition = LevelGrid.meuGrid.GetGridPosition(transform.position);

        if(!gridText || gridPosition.IsUnityNull())return;
        ChangeTextVisual(gridText);
    }
    private void LateUpdate()
    {
        ChangeTextVisual(gridText);
    }

    private void ChangeTextVisual(TextMeshPro textMeshPro)
    {
         textMeshPro.text = gridPosition.ToString();
    }
}
