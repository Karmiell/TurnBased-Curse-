using UnityEngine;
using UnityEngine.UI;

public class ActionUiHandler : MonoBehaviour
{
  [SerializeField]private Transform actionUiSingle;
  [SerializeField]private Transform counteinerButtons;

    private void Start()
    {
        HandlerSelection.Instance.OnAtualSelect += HandlerSelection_OnAtualSelect;
    }

    private void HandlerSelection_OnAtualSelect(bool a)
    {
        CreateButtons(actionUiSingle);
    }

    private void CreateButtons(Transform actionUiSingle)
    {
        foreach(Transform atual in counteinerButtons)
        {
            Destroy(atual.gameObject);
        }

        foreach(var atual in HandlerSelection.Instance.GetSelectUnit().GetBaseActionsArray())
        {
            var button = Instantiate(actionUiSingle, counteinerButtons);
            var buttonUI = button.GetComponent<ActionUiSingle>();

            buttonUI.SetBaseAction(atual);
        }
    }
}
