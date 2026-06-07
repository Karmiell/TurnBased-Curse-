using UnityEngine;

public class SelectVisual : MonoBehaviour
{
   [SerializeField]private GameObject visualSelect;
   [SerializeField]private Unit unit;
private void Awake()
    {
        unit = gameObject.GetComponentInParent<Unit>();
    }
private void Start()
    {
        HandlerSelection.Instance.OnAtualSelect += ChangeVisual;
   
        Hide();
    }
private void ChangeVisual(bool SelectVisual)
    {
        if(SelectVisual && unit == HandlerSelection.Instance.GetSelectUnit())Show();
        else Hide();
    }

    private void Show() => visualSelect.SetActive(true);
    private void Hide()=> visualSelect.SetActive(false);
}
